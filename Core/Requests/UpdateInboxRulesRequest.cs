// ---------------------------------------------------------------------------
// <copyright file="UpdateInboxRulesRequest.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

//-----------------------------------------------------------------------
// <summary>Defines the UpdateInboxRulesRequest class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a UpdateInboxRulesRequest request.
    /// </summary>
    internal sealed class UpdateInboxRulesRequest : SimpleServiceRequestBase
    {
        /// <summary>
        /// The smtp address of the mailbox from which to get the inbox rules.
        /// </summary>
        private string mailboxSmtpAddress;
        
        /// <summary>
        /// Remove OutlookRuleBlob or not.
        /// </summary>
        private bool removeOutlookRuleBlob;

        /// <summary>
        /// InboxRule operation collection.
        /// </summary>
        private IEnumerable<RuleOperation> inboxRuleOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInboxRulesRequest"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        internal UpdateInboxRulesRequest(ExchangeService service)
            : base(service)
        {
        }

        /// <summary>
        /// Gets the name of the XML element.
        /// </summary>
        /// <returns>XML element name.</returns>
        internal override string GetXmlElementName()
        {
            return XmlElementNames.UpdateInboxRules;
        }

        /// <summary>
        /// Writes XML elements.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.mailboxSmtpAddress))
            {
                writer.WriteElementValue(
                    XmlNamespace.Messages,
                    XmlElementNames.MailboxSmtpAddress,
                    this.mailboxSmtpAddress);
            }
            
            writer.WriteElementValue(
                XmlNamespace.Messages,
                XmlElementNames.RemoveOutlookRuleBlob, 
                this.RemoveOutlookRuleBlob);
            writer.WriteStartElement(XmlNamespace.Messages, XmlElementNames.Operations);
            foreach (RuleOperation operation in this.inboxRuleOperations)
            {
                operation.WriteToXml(writer, operation.XmlElementName);
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// Gets the name of the response XML element.
        /// </summary>
        /// <returns>XML element name.</returns>
        internal override string GetResponseXmlElementName()
        {
            return XmlElementNames.UpdateInboxRulesResponse;
        }

        /// <summary>
        /// Parses the response.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>Response object.</returns>
        internal override object ParseResponse(EwsServiceXmlReader reader)
        {
            UpdateInboxRulesResponse response = new UpdateInboxRulesResponse();
            response.LoadFromXml(reader, XmlElementNames.UpdateInboxRulesResponse);
            return response;
        }

        /// <summary>
        /// Gets the request version.
        /// </summary>
        /// <returns>Earliest Exchange version in which this request is supported.</returns>
        internal override ExchangeVersion GetMinimumRequiredServerVersion()
        {
            return ExchangeVersion.Exchange2010_SP1;
        }

        /// <summary>
        /// Validate request.
        /// </summary>
        internal override void Validate()
        {
            if (this.inboxRuleOperations == null)
            {
                throw new ArgumentException("RuleOperations cannot be null.", "Operations");
            }

            int operationCount = 0;
            foreach (RuleOperation operation in this.inboxRuleOperations)
            {
                EwsUtilities.ValidateParam(operation, "RuleOperation");
                operationCount++;
            }

            if (operationCount == 0)
            {
                throw new ArgumentException("RuleOperations cannot be empty.", "Operations");
            }

            this.Service.Validate();
        }

        /// <summary>
        /// Executes this request.
        /// </summary>
        /// <returns>Service response.</returns>
        internal UpdateInboxRulesResponse Execute()
        {
            UpdateInboxRulesResponse serviceResponse = (UpdateInboxRulesResponse)this.InternalExecute();
            if (serviceResponse.Result == ServiceResult.Error)
            {
                throw new UpdateInboxRulesException(serviceResponse, this.inboxRuleOperations.GetEnumerator());
            }
            return serviceResponse;
        }
        
        /// <summary>
        /// Gets or sets the address of the mailbox in which to update the inbox rules.
        /// </summary>
        internal string MailboxSmtpAddress
        {
            get { return this.mailboxSmtpAddress; }
            set { this.mailboxSmtpAddress = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to remove OutlookRuleBlob from
        /// the rule collection.
        /// </summary>
        internal bool RemoveOutlookRuleBlob
        {
            get { return this.removeOutlookRuleBlob; }
            set { this.removeOutlookRuleBlob = value; }
        }

        /// <summary>
        /// Gets or sets the RuleOperation collection.
        /// </summary>
        internal IEnumerable<RuleOperation> InboxRuleOperations
        {
            get { return this.inboxRuleOperations; }
            set { this.inboxRuleOperations = value; }
        }
    }
}