#define TRACE
using Microsoft.MetadirectoryServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

namespace FimSync_Ezma
{
    //
    class ConstDefinition
    {
        public const string CFG_RESOURCE_URI = "Resource URI";
        public const string CFG_AUTH_TOKEN = "Auth Token";

        public const int    CFG_IMPORT_MAX_PAGE_SIZE = 100;
        public const int    CFG_IMPORT_DEFAULT_PAGE_SIZE = 100;
        public const int    CFG_EXPORT_MAX_PAGE_SIZE = 100;
        public const int    CFG_EXPORT_DEFAULT_PAGE_SIZE = 100;

        public const string MSG0000_START_CAPABILITIES = "Starting Capabilities";
        public const string MSG0100_START_GETSCHEMA = "Starting GetSchema";
        public const string MSG0150_VERBOSE_GETSCHEMA = "GetSchema : ";
        public const string MSG0199_ERROR_GETSCHEMA = "Error in GetSchema : ";
        public const string MSG0200_START_GETCONFIGPARAMETERS = "Starting GetConfigParameters";
        public const string MSG0299_ERROR_GETCONFIGPARAMETERS = " Error in GetConfigParameters : ";
        public const string MSG0300_START_VALIDATECONFIGPARAMETERS = "Starting ValidateConfigParameters";
        public const string MSG0399_ERROR_VALIDATECONFIGPARAMETERS = "Error in ValidateConfigParameters : ";
        public const string MSG0400_START_OPENIMPORTCONNECTION = "Starting OpenImportConnection";
        public const string MSG0450_VERBOSE_OPENIMPORTCONNECTION = "OpenImportConnection : ";
        public const string MSG0499_ERROR_OPENIMPORTCONNECTION = "Error in OpenImportConnection : ";
        public const string MSG0500_START_GETIMPORTENTRIES = "Starting GetImportEntries";
        public const string MSG0550_VERBOSE_GETIMPORTENTRIES = "GetImportEntries : ";
        public const string MSG0599_ERROR_GETIMPORTENTRIES = "Error in GetImportEntries : ";
        public const string MSG0600_START_CLOSEIMPORTCONNECTION = "Starting CloseImportConnection";
        public const string MSG0650_VERBOSE_CLOSEIMPORTCONNECTION = "CloseImportConnection : ";
        public const string MSG0700_START_OPENEXPORTCONNECTION = "Starting OpenExportConnection";
        public const string MSG0750_VERBOSE_OPENEXPORTCONNECTION = "OpenExportConnection : ";
        public const string MSG0799_ERROR_OPENEXPORTCONNECTION = "Error in OpenExportConnection : ";
        public const string MSG0800_START_PUTEXPORTENTRIES = "Starting PutExportEntries";
        public const string MSG0850_VERBOSE_PUTEXPORTENTRIES = "PutExportEntries : ";
        public const string MSG0899_ERROR_PUTEXPORTENTRIES = "Error in PutExportEntries : ";
        public const string MSG0900_START_CLOSEEXPORTCONNECTION = "Starting CloseExportConnection";
        public const string MSG1000_START_OPENPASSWORDCONNECTION = "Starting OpenPasswordConnection";
        public const string MSG1050_VERBOSE_OPENPASSWORDCONNECTION = "OpenPasswordConnection : ";
        public const string MSG1099_ERROR_OPENPASSWORDCONNECTION = "Error in OpenPasswordConnection : ";
        public const string MSG1100_START_CLOSEPASSWORDCONNECTION = "Starting ClosePasswordConnection";
        public const string MSG1200_START_SETPASSWORD = "Starting SetPassword";
        public const string MSG1299_ERROR_SETPASSWORD = "Error in SetPassword : ";
        public const string MSG1300_START_CHANGEPASSWORD = "Starting ChangePassword";
        public const string MSG1399_ERROR_CHANGEPASSWORD = "Error in ChangePassword : ";
        public const string MSG1400_START_REQUIRECHANGEPASSWORDONNEXTLOGIN = "Starting RequireChangePasswordOnNextLogin";
        public const string MSG1499_ERROR_REQUIRECHANGEPASSWORDONNEXTLOGIN = "Error in RequireChangePasswordOnNextLogin : ";
        public const string MSG1500_START_PUTEXPORTENTRIESMEMBERS = "Starting PutExportEntriesMembers";
        public const string MSG1550_VERBOSE_PUTEXPORTENTRIESMEMBERS = "PutExportEntriesMembers : ";
        public const string MSG1599_ERROR_PUTEXPORTENTRIESMEMBERS = "Error in PutExportEntriesMembers : ";

        public const string MSG1600_START_PUTEXPORTENTRIES_ADD = "Starting PutExportEntriesAdd";
        public const string MSG1699_ERROR_PUTEXPORTENTRIES_ADD = "Error in PutExportEntriesAdd : ";
        public const string MSG1700_START_PUTEXPORTENTRIES_UPDATE = "Starting PutExportEntriesUpdate";
        public const string MSG1799_ERROR_PUTEXPORTENTRIES_UPDATE = "Error in PutExportEntriesUpdate : ";
        public const string MSG1800_START_PUTEXPORTENTRIES_DELETE = "Starting PutExportEntriesDelete";
        public const string MSG1850_VERBOSE_PUTEXPORTENTRIES_DELETE = "PutExportEntriesDelete : ";
        public const string MSG1899_ERROR_PUTEXPORTENTRIES_DELETE = "Error in PutExportEntriesDelete : ";
        public const string MSG1900_START_PUTEXPORTENTRIESMEMBERS_ADD = "Starting PutExportEntriesMembersAdd";
        public const string MSG1999_ERROR_PUTEXPORTENTRIESMEMBERS_ADD = "Error in PutExportEntriesMembersAdd : ";
        public const string MSG2000_START_PUTEXPORTENTRIESMEMBERS_DELETE = "Starting PutExportEntriesMembersDelete";
        public const string MSG2098_WARNING_PUTEXPORTENTRIESMEMBERS_DELETE = "Warning in PutExportEntriesMembersDelete : ";
        public const string MSG2099_ERROR_PUTEXPORTENTRIESMEMBERS_DELETE = "Error in PutExportEntriesMembersDelete : ";
        public const string MSG2100_START_PUTEXPORTENTRIESMEMBERS_DELETEALL = "Starting PutExportEntriesMembersDeleteAll";
        public const string MSG2150_VERBOSE_PUTEXPORTENTRIESMEMBERS_DELETEALL = "PutExportEntriesMembersDeleteAll : ";

        public const int    ID0000_START_CAPABILITIES = 0;
        public const int    ID0100_START_GETSCHEMA = 100;
        public const int    ID0150_VERBOSE_GETSCHEMA = 150;
        public const int    ID0199_ERROR_GETSCHEMA = 199;
        public const int    ID0200_START_GETCONFIGPARAMETERS = 200;
        public const int    ID0299_ERROR_GETCONFIGPARAMETERS = 299;
        public const int    ID0300_START_VALIDATECONFIGPARAMETERS = 300;
        public const int    ID0399_ERROR_VALIDATECONFIGPARAMETERS = 399;
        public const int    ID0400_START_OPENIMPORTCONNECTION = 400;
        public const int    ID0450_VERBOSE_OPENIMPORTCONNECTION = 450;
        public const int    ID0499_ERROR_OPENIMPORTCONNECTION = 499;
        public const int    ID0500_START_GETIMPORTENTRIES = 500;
        public const int    ID0550_VERBOSE_GETIMPORTENTRIES = 550;
        public const int    ID0599_ERROR_GETIMPORTENTRIES = 599;
        public const int    ID0600_START_CLOSEIMPORTCONNECTION = 600;
        public const int    ID0650_VERBOSE_CLOSEIMPORTCONNECTION = 650;
        public const int    ID0699_ERROR_CLOSEIMPORTCONNECTION = 699;
        public const int    ID0700_START_OPENEXPORTCONNECTION = 700;
        public const int    ID0750_VERBOSE_OPENEXPORTCONNECTION = 750;
        public const int    ID0799_ERROR_OPENEXPORTCONNECTION = 799;
        public const int    ID0800_START_PUTEXPORTENTRIES = 800;
        public const int    ID0850_VERBOSE_PUTEXPORTENTRIES = 850;
        public const int    ID0899_ERROR_PUTEXPORTENTRIES = 899;
        public const int    ID0900_START_CLOSEEXPORTCONNECTION = 900;
        public const int    ID1000_START_OPENPASSWORDCONNECTION = 1000;
        public const int    ID1050_VERBOSE_OPENPASSWORDCONNECTION = 1050;
        public const int    ID1099_ERROR_OPENPASSWORDCONNECTION = 1099;
        public const int    ID1100_START_CLOSEPASSWORDCONNECTION = 1100;
        public const int    ID1200_START_SETPASSWORD = 1200;
        public const int    ID1299_ERROR_SETPASSWORD = 1299;
        public const int    ID1300_START_CHANGEPASSWORD = 1300;
        public const int    ID1399_ERROR_CHANGEPASSWORD = 1399;
        public const int    ID1400_START_REQUIRECHANGEPASSWORDONNEXTLOGIN = 1400;
        public const int    ID1499_ERROR_REQUIRECHANGEPASSWORDONNEXTLOGIN = 1499;
        public const int    ID1500_START_PUTEXPORTENTRIESMEMBERS = 1500;
        public const int    ID1550_VERBOSE_PUTEXPORTENTRIESMEMBERS = 1550;
        public const int    ID1599_ERROR_PUTEXPORTENTRIESMEMBERS = 1599;        
        public const int    ID1600_START_PUTEXPORTENTRIES_ADD = 1600;
        public const int    ID1699_ERROR_PUTEXPORTENTRIES_ADD = 1699;
        public const int    ID1700_START_PUTEXPORTENTRIES_UPDATE = 1700;
        public const int    ID1799_ERROR_PUTEXPORTENTRIES_UPDATE = 1799;
        public const int    ID1800_START_PUTEXPORTENTRIES_DELETE = 1800;
        public const int    ID1850_VERBOSE_PUTEXPORTENTRIES_DELETE = 1850;
        public const int    ID1899_ERROR_PUTEXPORTENTRIES_DELETE = 1899;
        public const int    ID1900_START_PUTEXPORTENTRIESMEMBERS_ADD = 1900;
        public const int    ID1999_ERROR_PUTEXPORTENTRIESMEMBERS_ADD = 1999;
        public const int    ID2000_START_PUTEXPORTENTRIESMEMBERS_DELETE = 2000;
        public const int    ID2098_WARNING_PUTEXPORTENTRIESMEMBERS_DELETE = 2098;
        public const int    ID2099_ERROR_PUTEXPORTENTRIESMEMBERS_DELETE = 2099;
        public const int    ID2100_START_PUTEXPORTENTRIESMEMBERS_DELETEALL = 2100;
        public const int    ID2150_VERBOSE_PUTEXPORTENTRIESMEMBERS_DELETEALL = 2150;
    }

    public class EzmaExtension :
    IMAExtensible2CallExport,
    IMAExtensible2CallImport,
    IMAExtensible2GetSchema,
    IMAExtensible2GetCapabilities,
    IMAExtensible2GetParameters,
    IMAExtensible2Password
    {
        //
        // global variables
        //
        OperationType operationType;
        string resource_uri = null;
        string auth_token = null;
        // utility
        Utils utils;

        public EzmaExtension()
        {
            // Create Utility Instance
            utils = new Utils();
        }
        public MACapabilities Capabilities
        {
            get
            {
                utils.Logger(TraceEventType.Information,
                             ConstDefinition.ID0000_START_CAPABILITIES,
                             ConstDefinition.MSG0000_START_CAPABILITIES);
                return new MACapabilities
                {
                    ObjectRename = false,
                    ObjectConfirmation = MAObjectConfirmation.Normal,
                    DeleteAddAsReplace = true,
                    DeltaImport = false,
                    DistinguishedNameStyle = MADistinguishedNameStyle.None,
                    ExportType = MAExportType.AttributeUpdate,
                    NoReferenceValuesInFirstExport = false,
                    FullExport = true,
                    Normalizations = MANormalizations.None
                };
            }
        }
        // Get Schema
        public Schema GetSchema(KeyedCollection<string, ConfigParameter> _configParameters)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0100_START_GETSCHEMA,
                         ConstDefinition.MSG0100_START_GETSCHEMA);
            try
            {
                SchemaType personType = SchemaType.Create("Person", false);
                personType.Attributes.Add(SchemaAttribute.CreateAnchorAttribute("Username", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("Alias", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("Email", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("LastName", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("EmailEncodingKey", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("LanguageLocaleKey", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("LocaleSidKey", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("ProfileId", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("TimeZoneSidKey", AttributeType.String));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("UserPermissionsOfflineUser", AttributeType.Boolean));
                personType.Attributes.Add(SchemaAttribute.CreateSingleValuedAttribute("UserPermissionsMarketingUser", AttributeType.Boolean));
                Schema schema = Schema.Create();
                schema.Types.Add(personType);
                return schema;
            }
            catch (Exception ex)
            {
                utils.Logger(TraceEventType.Error,
                             ConstDefinition.ID0199_ERROR_GETSCHEMA,
                             ConstDefinition.MSG0199_ERROR_GETSCHEMA + ex.Message);
                throw new ExtensibleExtensionException(ConstDefinition.MSG0199_ERROR_GETSCHEMA + ex.Message);
            }
        }
        // Get Parameters
        public IList<ConfigParameterDefinition> GetConfigParameters(KeyedCollection<string, ConfigParameter> _configParameters, ConfigParameterPage _page)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0200_START_GETCONFIGPARAMETERS,
                         ConstDefinition.MSG0200_START_GETCONFIGPARAMETERS);
            var _configParametersDefinitions = new List<ConfigParameterDefinition>();
            try
            {
                switch (_page)
                {
                    case ConfigParameterPage.Capabilities:
                        break;
                    case ConfigParameterPage.Connectivity:
                        _configParametersDefinitions.Add(ConfigParameterDefinition.CreateLabelParameter("Connection parameters"));
                        _configParametersDefinitions.Add(ConfigParameterDefinition.CreateStringParameter(ConstDefinition.CFG_RESOURCE_URI, ""));
                        _configParametersDefinitions.Add(ConfigParameterDefinition.CreateStringParameter(ConstDefinition.CFG_AUTH_TOKEN, ""));
                        break;
                    case ConfigParameterPage.Global:
                        break;
                    case ConfigParameterPage.Partition:
                        break;
                    case ConfigParameterPage.RunStep:
                        break;
                    case ConfigParameterPage.Schema:
                        break;
                }
            }
            catch (Exception ex)
            {
                utils.Logger(TraceEventType.Error,
                             ConstDefinition.ID0299_ERROR_GETCONFIGPARAMETERS,
                             ConstDefinition.MSG0299_ERROR_GETCONFIGPARAMETERS + ex.Message);
                throw new ExtensibleExtensionException(ConstDefinition.MSG0299_ERROR_GETCONFIGPARAMETERS + ex.Message);
            }
            return _configParametersDefinitions;
        }
        // Validate Parameters
        public ParameterValidationResult ValidateConfigParameters(KeyedCollection<string, ConfigParameter> _configParameters, ConfigParameterPage _page)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0300_START_VALIDATECONFIGPARAMETERS,
                         ConstDefinition.MSG0300_START_VALIDATECONFIGPARAMETERS);
            try
            {
                switch (_page)
                {
                    case ConfigParameterPage.Connectivity:
                        break;
                    case ConfigParameterPage.Global:
                        break;
                    case ConfigParameterPage.Partition:
                        break;
                    case ConfigParameterPage.RunStep:
                        break;
                    case ConfigParameterPage.Schema:
                        break;
                }
                return new ParameterValidationResult();
            }
            catch (Exception ex)
            {
                utils.Logger(TraceEventType.Error,
                             ConstDefinition.ID0399_ERROR_VALIDATECONFIGPARAMETERS,
                             ConstDefinition.MSG0300_START_VALIDATECONFIGPARAMETERS + ex.Message);
                throw new ExtensibleExtensionException(ConstDefinition.MSG0399_ERROR_VALIDATECONFIGPARAMETERS + ex.Message);
            }
        }
        //
        // Import
        //
        // OpenImportConnection
        public OpenImportConnectionResults OpenImportConnection(
                                       KeyedCollection<string, ConfigParameter> _configParameters,
                                       Schema _types,
                                       OpenImportConnectionRunStep _importRunStep)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0400_START_OPENIMPORTCONNECTION,
                         ConstDefinition.MSG0400_START_OPENIMPORTCONNECTION);
            try
            {
                // Get OperationType
                operationType = _importRunStep.ImportType;
#if DEBUG
                switch (operationType)
                {
                    case OperationType.Full:
                        utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0450_VERBOSE_OPENIMPORTCONNECTION, "OperationType : Full");
                        break;
                    case OperationType.Delta:
                        utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0450_VERBOSE_OPENIMPORTCONNECTION, "OperationType : Delta");
                        break;
                    case OperationType.FullObject:
                        utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0450_VERBOSE_OPENIMPORTCONNECTION, "OperationType : FullObject");
                        break;
                    default:
                        utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0450_VERBOSE_OPENIMPORTCONNECTION, "OperationType : Other");
                        break;
                }
#endif
                resource_uri = _configParameters[ConstDefinition.CFG_RESOURCE_URI].Value.ToString();
                auth_token = _configParameters[ConstDefinition.CFG_AUTH_TOKEN].Value.ToString();
                return new OpenImportConnectionResults();
            }
            catch (Exception ex)
            {
                utils.Logger(TraceEventType.Error,
                             ConstDefinition.ID0499_ERROR_OPENIMPORTCONNECTION,
                             ex.Message);
                throw new ExtensibleExtensionException(ConstDefinition.MSG0499_ERROR_OPENIMPORTCONNECTION + ex.Message);
            }
        }
        // GetImportEntries
        public GetImportEntriesResults GetImportEntries(GetImportEntriesRunStep _importRunStep)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0500_START_GETIMPORTENTRIES,
                         ConstDefinition.MSG0500_START_GETIMPORTENTRIES);
            try
            {
                var _csentries = new List<CSEntryChange>();
                string _importEntriesJSON = utils.GetContentsWithAccessToken(resource_uri, auth_token, null);
                var _getImportEntriesByObjectTypeResult = JObject.Parse(_importEntriesJSON).SelectToken("value").ToString();
                var _importObjectJSONArray = JArray.Parse(_getImportEntriesByObjectTypeResult);
                foreach (var _importObjectJSON in _importObjectJSONArray)
                {
                    var _csentryChange = CSEntryChange.Create();
                    _csentryChange.ObjectModificationType = ObjectModificationType.Add;
                    _csentryChange.ObjectType = "Person";
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("Alias", _importObjectJSON["Alias"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("Email", _importObjectJSON["Email"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("Username", _importObjectJSON["Username"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("LastName", _importObjectJSON["LastName"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("EmailEncodingKey", _importObjectJSON["EmailEncodingKey"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("LanguageLocaleKey", _importObjectJSON["LanguageLocaleKey"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("LocaleSidKey", _importObjectJSON["LocaleSidKey"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("ProfileId", _importObjectJSON["ProfileId"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("TimeZoneSidKey", _importObjectJSON["TimeZoneSidKey"].ToString()));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("UserPermissionsOfflineUser", false));
                    _csentryChange.AttributeChanges.Add(AttributeChange.CreateAttributeAdd("UserPermissionsMarketingUser", false));
                    _csentries.Add(_csentryChange);
                }
                var _importReturnInfo = new GetImportEntriesResults();
                _importReturnInfo.MoreToImport = false;
                _importReturnInfo.CSEntries = _csentries;
                return _importReturnInfo;
            }
            catch (Exception ex)
            {
                utils.Logger(TraceEventType.Error,
                             ConstDefinition.ID0599_ERROR_GETIMPORTENTRIES,
                             ConstDefinition.MSG0599_ERROR_GETIMPORTENTRIES + ex.Message);
                throw new ExtensibleExtensionException(ConstDefinition.MSG0599_ERROR_GETIMPORTENTRIES + ex.Message);
            }
        }
        // CloseImportConnection
        public CloseImportConnectionResults CloseImportConnection(CloseImportConnectionRunStep _importRunStepInfo)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0600_START_CLOSEIMPORTCONNECTION,
                         ConstDefinition.MSG0600_START_CLOSEIMPORTCONNECTION);
            return new CloseImportConnectionResults();
        }
        // other
        public int ImportMaxPageSize
        {
            get
            {
                return ConstDefinition.CFG_IMPORT_MAX_PAGE_SIZE;
            }
        }
        public int ImportDefaultPageSize
        {
            get
            {
                return ConstDefinition.CFG_IMPORT_DEFAULT_PAGE_SIZE;
            }
        }
        //
        // Export
        //
        // OpenExportConnection
        public void OpenExportConnection(KeyedCollection<string, ConfigParameter> _configParameters,
                                         Schema _types,
                                         OpenExportConnectionRunStep _exportRunStep)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0700_START_OPENEXPORTCONNECTION,
                         ConstDefinition.MSG0700_START_OPENEXPORTCONNECTION);
            resource_uri = _configParameters[ConstDefinition.CFG_RESOURCE_URI].Value.ToString();
            auth_token = _configParameters[ConstDefinition.CFG_AUTH_TOKEN].Value.ToString();
        }
        // PutExportEntries
        public PutExportEntriesResults PutExportEntries(IList<CSEntryChange> _csentries)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0800_START_PUTEXPORTENTRIES,
                         ConstDefinition.MSG0800_START_PUTEXPORTENTRIES);
            PutExportEntriesResults _exportEntriesResults = new PutExportEntriesResults();
            try
            {
                foreach (CSEntryChange _csentryChange in _csentries)
                {
#if DEBUG
                    utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0850_VERBOSE_PUTEXPORTENTRIES, _csentryChange.DN.ToString());
#endif
                    // build json to POST
                    var _attr = new Dictionary<string, string>();
                    // anchor attribute
                    _attr.Add("Username", _csentryChange.DN.ToString());
                    switch (_csentryChange.ObjectModificationType)
                    {
                        case ObjectModificationType.Add:
                        case ObjectModificationType.Update:
#if DEBUG
                        utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0850_VERBOSE_PUTEXPORTENTRIES, "ObjectModificationType : Add / Update");
#endif
                        foreach (string _attribName in _csentryChange.ChangedAttributeNames)
                        {
                            var _attributeChange = _csentryChange.AttributeChanges[_attribName];
                            var _valueChanges = _attributeChange.ValueChanges;
                            if (_valueChanges != null)
                            {
                                foreach (var _valueChange in _valueChanges)
                                {
                                    if (_valueChange.ModificationType == ValueModificationType.Add)
                                    {
                                        // new value
#if DEBUG
                                        utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0850_VERBOSE_PUTEXPORTENTRIES, _attribName + " : " + _valueChange.Value.ToString());
#endif
                                        _attr.Add(_attribName, _valueChange.Value.ToString());
                                        break;
                                        }
                                    }
                                }
                            }
                            // build json
                            string _exportDataJSON = JsonConvert.SerializeObject(_attr);
#if DEBUG
                            utils.Logger(TraceEventType.Verbose, ConstDefinition.ID0850_VERBOSE_PUTEXPORTENTRIES, _exportDataJSON);
#endif
                            string _exportResult = utils.PostContentsWithAccessToken(resource_uri, auth_token, _exportDataJSON, null);

                            _exportEntriesResults.CSEntryChangeResults.Add(
                                CSEntryChangeResult.Create(_csentryChange.Identifier,
                                                           _csentryChange.AttributeChanges,
                                                           MAExportError.Success));
                                break;
                            case ObjectModificationType.Delete:
                                // NOT Implemented
                                break;
                            default:
                                // error
                                utils.Logger(TraceEventType.Error,
                                             ConstDefinition.ID0899_ERROR_PUTEXPORTENTRIES,
                                             ConstDefinition.MSG0899_ERROR_PUTEXPORTENTRIES + "Unknown Operation Type : " + _csentryChange.ObjectModificationType);
                                _exportEntriesResults.CSEntryChangeResults.Add(
                                    CSEntryChangeResult.Create(_csentryChange.Identifier,
                                                                _csentryChange.AttributeChanges,
                                                                MAExportError.ExportErrorConnectedDirectoryError,
                                                                "Operation Error",
                                                                "Unknown Operation Type : " + _csentryChange.ObjectModificationType
                                                                ));
                                break;
                        }
                }
                return _exportEntriesResults;
            }
            catch (Exception ex)
            {
                utils.Logger(TraceEventType.Error,
                             ConstDefinition.ID0899_ERROR_PUTEXPORTENTRIES,
                             ConstDefinition.MSG0899_ERROR_PUTEXPORTENTRIES + ex.Message);
                throw new ExtensibleExtensionException(ex.Message);
            }
        }

        // CloseExportConnection
        public void CloseExportConnection(CloseExportConnectionRunStep _exportRunStep)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID0900_START_CLOSEEXPORTCONNECTION,
                         ConstDefinition.MSG0900_START_CLOSEEXPORTCONNECTION);
        }
        public int ExportDefaultPageSize
        {
            get
            {
                return ConstDefinition.CFG_EXPORT_DEFAULT_PAGE_SIZE;
            }
        }
        public int ExportMaxPageSize
        {
            get
            {
                return ConstDefinition.CFG_EXPORT_MAX_PAGE_SIZE;
            }
        }
        // Password Management
        public void OpenPasswordConnection(KeyedCollection<string, ConfigParameter> _configParameters, Partition _partition)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID1000_START_OPENPASSWORDCONNECTION,
                         ConstDefinition.MSG1000_START_OPENPASSWORDCONNECTION);
        }
        public void ClosePasswordConnection()
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID1100_START_CLOSEPASSWORDCONNECTION,
                         ConstDefinition.MSG1100_START_CLOSEPASSWORDCONNECTION);
        }
        public ConnectionSecurityLevel GetConnectionSecurityLevel()
        {
            return ConnectionSecurityLevel.Secure;
        }
        private void changePassword(CSEntry _csentry, System.Security.SecureString _newPassword)
        {
        }
        public void SetPassword(CSEntry _csentry, System.Security.SecureString _newPassword, PasswordOptions _options)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID1200_START_SETPASSWORD,
                         ConstDefinition.MSG1200_START_SETPASSWORD);
        }
        public void ChangePassword(CSEntry _csentry, System.Security.SecureString _oldPassword, System.Security.SecureString _newPassword)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID1300_START_CHANGEPASSWORD,
                         ConstDefinition.MSG1300_START_CHANGEPASSWORD);
        }
        public void RequireChangePasswordOnNextLogin(CSEntry _csentry,bool _fRequireChangePasswordOnNextLogin)
        {
            utils.Logger(TraceEventType.Information,
                         ConstDefinition.ID1400_START_REQUIRECHANGEPASSWORDONNEXTLOGIN,
                         ConstDefinition.MSG1400_START_REQUIRECHANGEPASSWORDONNEXTLOGIN);
            // To do : implement
            throw new ExtensibleExtensionException(ConstDefinition.MSG1499_ERROR_REQUIRECHANGEPASSWORDONNEXTLOGIN + "Not Implemented");
        }
    };
}
