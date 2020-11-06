// ReSharper disable InconsistentNaming - follows SDK naming convention

using JetBrains.Annotations;

namespace StreamDeck.NET
{
    /// <summary>
    /// Various constants as defined in the Stream Deck SDK.
    /// </summary>
    #pragma warning disable 1591
    [PublicAPI]
    public static class StreamDeckConsts
    {
        //
        // Current version of the SDK
        //
        public const int kESDSDKVersion = 2;


        //
        // Common base-interface
        //
        public const string kESDSDKCommonAction = "action";
        public const string kESDSDKCommonEvent = "event";
        public const string kESDSDKCommonContext = "context";
        public const string kESDSDKCommonPayload = "payload";
        public const string kESDSDKCommonDevice = "device";
        public const string kESDSDKCommonDeviceInfo = "deviceInfo";


        //
        // Functions
        //
        public const string kESDSDKEventSetTitle = "setTitle";
        public const string kESDSDKEventSetImage = "setImage";
        public const string kESDSDKEventShowAlert = "showAlert";
        public const string kESDSDKEventShowOK = "showOk";
        public const string kESDSDKEventGetSettings = "getSettings";
        public const string kESDSDKEventSetSettings = "setSettings";
        public const string kESDSDKEventGetGlobalSettings = "getGlobalSettings";
        public const string kESDSDKEventSetGlobalSettings = "setGlobalSettings";
        public const string kESDSDKEventSetState = "setState";
        public const string kESDSDKEventSwitchToProfile = "switchToProfile";
        public const string kESDSDKEventSendToPropertyInspector = "sendToPropertyInspector";
        public const string kESDSDKEventSendToPlugin = "sendToPlugin";
        public const string kESDSDKEventOpenURL = "openUrl";
        public const string kESDSDKEventLogMessage = "logMessage";


        //
        // Payloads
        //
        public const string kESDSDKPayloadSettings = "settings";
        public const string kESDSDKPayloadCoordinates = "coordinates";
        public const string kESDSDKPayloadState = "state";
        public const string kESDSDKPayloadUserDesiredState = "userDesiredState";
        public const string kESDSDKPayloadTitle = "title";
        public const string kESDSDKPayloadTitleParameters = "titleParameters";
        public const string kESDSDKPayloadImage = "image";
        public const string kESDSDKPayloadURL = "url";
        public const string kESDSDKPayloadTarget = "target";
        public const string kESDSDKPayloadProfile = "profile";
        public const string kESDSDKPayloadApplication = "application";
        public const string kESDSDKPayloadIsInMultiAction = "isInMultiAction";
        public const string kESDSDKPayloadMessage = "message";

        public const string kESDSDKPayloadCoordinatesColumn = "column";
        public const string kESDSDKPayloadCoordinatesRow = "row";


        //
        // Device Info
        //
        public const string kESDSDKDeviceInfoID = "id";
        public const string kESDSDKDeviceInfoType = "type";
        public const string kESDSDKDeviceInfoSize = "size";
        public const string kESDSDKDeviceInfoName = "name";

        public const string kESDSDKDeviceInfoSizeColumns = "columns";
        public const string kESDSDKDeviceInfoSizeRows = "rows";


        //
        // Title Parameters
        //
        public const string kESDSDKTitleParametersShowTitle = "showTitle";
        public const string kESDSDKTitleParametersTitleColor = "titleColor";
        public const string kESDSDKTitleParametersTitleAlignment = "titleAlignment";
        public const string kESDSDKTitleParametersFontFamily = "fontFamily";
        public const string kESDSDKTitleParametersFontSize = "fontSize";
        public const string kESDSDKTitleParametersCustomFontSize = "customFontSize";
        public const string kESDSDKTitleParametersFontStyle = "fontStyle";
        public const string kESDSDKTitleParametersFontUnderline = "fontUnderline";


        //
        // Connection
        //
        public const string kESDSDKConnectSocketFunction = "connectElgatoStreamDeckSocket";
        public const string kESDSDKRegisterPlugin = "registerPlugin";
        public const string kESDSDKRegisterPropertyInspector = "registerPropertyInspector";
        public const string kESDSDKPortParameter = "-port";
        public const string kESDSDKPluginUUIDParameter = "-pluginUUID";
        public const string kESDSDKRegisterEventParameter = "-registerEvent";
        public const string kESDSDKInfoParameter = "-info";
        public const string kESDSDKRegisterUUID = "uuid";

        public const string kESDSDKApplicationInfo = "application";
        public const string kESDSDKPluginInfo = "plugin";
        public const string kESDSDKDevicesInfo = "devices";
        public const string kESDSDKColorsInfo = "colors";
        public const string kESDSDKDevicePixelRatio = "devicePixelRatio";

        public const string kESDSDKApplicationInfoVersion = "version";
        public const string kESDSDKApplicationInfoLanguage = "language";
        public const string kESDSDKApplicationInfoPlatform = "platform";

        public const string kESDSDKApplicationInfoPlatformMac = "mac";
        public const string kESDSDKApplicationInfoPlatformWindows = "windows";

        public const string kESDSDKColorsInfoHighlightColor = "highlightColor";
        public const string kESDSDKColorsInfoMouseDownColor = "mouseDownColor";
        public const string kESDSDKColorsInfoDisabledColor = "disabledColor";
        public const string kESDSDKColorsInfoButtonPressedTextColor = "buttonPressedTextColor";
        public const string kESDSDKColorsInfoButtonPressedBackgroundColor = "buttonPressedBackgroundColor";
        public const string kESDSDKColorsInfoButtonMouseOverBackgroundColor = "buttonMouseOverBackgroundColor";
        public const string kESDSDKColorsInfoButtonPressedBorderColor = "buttonPressedBorderColor";
    }


    [PublicAPI]
    public enum ESDSDKTarget
    {
        kESDSDKTarget_HardwareAndSoftware = 0,
        kESDSDKTarget_HardwareOnly = 1,
        kESDSDKTarget_SoftwareOnly = 2
    }


    [PublicAPI]
    public enum ESDSDKDeviceType
    {
        kESDSDKDeviceType_StreamDeck = 0,
        kESDSDKDeviceType_StreamDeckMini = 1,
        kESDSDKDeviceType_StreamDeckXL = 2,
        kESDSDKDeviceType_StreamDeckMobile = 3
    }


    [PublicAPI]
    public enum ESDSDKEventType
    {
        keyDown,
        keyUp,
        willAppear,
        willDisappear,
        deviceDidConnect,
        deviceDidDisconnect,
        applicationDidLaunch,
        applicationDidTerminate,
        systemDidWakeUp,
        titleParametersDidChange,
        didReceiveSettings,
        didReceiveGlobalSettings,
        propertyInspectorDidAppear,
        propertyInspectorDidDisappear
    }
    #pragma warning restore 1591
}