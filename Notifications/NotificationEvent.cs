// ---------------------------------------------------------------------------
// <copyright file="NotificationEvent.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

//-----------------------------------------------------------------------
// <summary>Defines the NotificationEvent class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    using System;

    /// <summary>
    /// Represents an event as exposed by push and pull notifications. 
    /// </summary>
    public abstract class NotificationEvent
    {
        /// <summary>
        /// Type of this event.
        /// </summary>
        private EventType eventType;

        /// <summary>
        /// Date and time when the event occurred.
        /// </summary>
        private DateTime timestamp;

        /// <summary>
        /// Id of parent folder of the item or folder this event applies to.
        /// </summary>
        private FolderId parentFolderId;

        /// <summary>
        /// Id of the old prarent foldero of the item or folder this event applies to.
        /// This property is only meaningful when EventType is equal to either EventType.Moved 
        /// or EventType.Copied. For all other event types, oldParentFolderId will be null.
        /// </summary>
        private FolderId oldParentFolderId;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEvent"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="timestamp">The event timestamp.</param>
        internal NotificationEvent(EventType eventType, DateTime timestamp)
        {
            this.eventType = eventType;
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Load from XML.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal virtual void InternalLoadFromXml(EwsServiceXmlReader reader)
        {
        }

        /// <summary>
        /// Loads this NotificationEvent from XML.
        /// </summary>
        /// <param name="reader">The reader from which to read the notification event.</param>
        /// <param name="xmlElementName">The start XML element name of this notification event.</param>
        internal void LoadFromXml(EwsServiceXmlReader reader, string xmlElementName)
        {
            this.InternalLoadFromXml(reader);

            reader.ReadEndElementIfNecessary(XmlNamespace.Types, xmlElementName);
        }

        /// <summary>
        /// Loads from json.
        /// </summary>
        /// <param name="jsonEvent">The json event.</param>
        /// <param name="service">The service.</param>
        internal abstract void LoadFromJson(JsonObject jsonEvent, ExchangeService service);

        /// <summary>
        /// Gets the type of this event.
        /// </summary>
        public EventType EventType
        {
            get { return this.eventType; }
        }

        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timestamp; }
        }

        /// <summary>
        /// Gets the Id of the parent folder of the item or folder this event applie to.
        /// </summary>
        public FolderId ParentFolderId
        {
            get { return this.parentFolderId; }
            internal set { this.parentFolderId = value; }
        }

        /// <summary>
        /// Gets the Id of the old parent folder of the item or folder this event applies to.
        /// OldParentFolderId is only meaningful when EventType is equal to either EventType.Moved or
        /// EventType.Copied. For all other event types, OldParentFolderId is null.
        /// </summary>
        public FolderId OldParentFolderId
        {
            get { return this.oldParentFolderId; }
            internal set { this.oldParentFolderId = value; }
        }
    }
}
