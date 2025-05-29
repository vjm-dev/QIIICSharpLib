using System.Numerics;
using System.Text;

namespace Q3CSharpLib
{
    /// <summary>Quake boolean type, just a synonym of boolean</summary>
    public readonly struct QBoolean(bool value)
    {
        private readonly bool _value = value;
        public static readonly QBoolean qtrue = true;
        public static readonly QBoolean qfalse = false;

        public static implicit operator bool(QBoolean q) => q._value;
        public static implicit operator QBoolean(bool b) => b;
    }

    /// <summary>CVar System</summary>
    public struct VmCvar
    {
        public QShared.CVarFlags handle; // cvarHandle_t
        public int modificationCount;
        public float value;
        public int integer;
        public string stringcvar;
        public string cvarDescription;

        public VmCvar()
        {
            handle = QShared.CVarFlags.CVAR_ARCHIVE;
            modificationCount = 0;
            value = 0.0f;
            integer = 0;
            stringcvar = "";
            cvarDescription = string.Empty;
        }
    }

    /// <summary>Orientation</summary>
    public struct Orientation
    {
        public Vector3 origin;
        public Vector3[] axis = new Vector3[3];

        public Orientation()
        {
            origin  = Vector3.Zero;
            axis[0] = Vector3.Zero;
            axis[1] = Vector3.Zero;
            axis[2] = Vector3.Zero;
        }
    }

    /// <summary>Quake Time struct format</summary>
    public struct QTime
    {
        public int tm_sec;   // Seconds after minute [0-59]
        public int tm_min;   // Minutes after hour [0-59]
        public int tm_hour;  // Hours since midnight [0-23]
        public int tm_mday;  // Day of month [1-31]
        public int tm_mon;   // Months since January [0-11]
        public int tm_year;  // Years since 1900
        public int tm_wday;  // Days since Sunday [0-6]
        public int tm_yday;  // Days since Jan 1 [0-365]
        public int tm_isdst; // Daylight savings flag
    }

    public static class QShared
    {
        /// <summary>Maximum integer size of a Quake game</summary>
        public const int MAX_QINT			= 0x7fffffff;

        /// <summary>Minimum integer size of a Quake game</summary>
        public const int MIN_QINT           = (-MAX_QINT - 1);

        /// <summary>Maximum unsigned integer size of a Quake game</summary>
        public const uint MAX_UINT          = uint.MaxValue;


        /// <summary>Maximum length of a Quake game pathname</summary>
        public const int MAX_QPATH          = 64;

        /// <summary>Maximum length of a filesystem pathname</summary>
        public const int MAX_OSPATH         = 256;

        /// <summary>Maximum length of a client name</summary>
        public const int MAX_NAME_LENGTH    = 32;

        /// <summary>Maximum length of chat text</summary>
        public const int MAX_SAY_TEXT       = 150;

        /// <summary>
        /// Gets the number of elements in a single-dimensional array
        /// </summary>
        /// <typeparam name="T">Array element type</typeparam>
        /// <param name="array">Input array</param>
        /// <returns>Total element count in the array</returns>
        public static int ARRAY_LEN<T>(T[] array) => array.Length;

        /// <summary>
        /// Gets the length of a null-terminated string array excluding the terminator
        /// </summary>
        /// <typeparam name="T">Array element type</typeparam>
        /// <param name="array">Input array (assumed to be null-terminated)</param>
        /// <returns>Number of elements before the null terminator</returns>
        public static int STRARRAY_LEN<T>(T[] array) => array.Length - 1;

        // the game guarantees that no string from the network will ever
        // exceed MAX_STRING_CHARS

        /// <summary>Max length of a string passed to Cmd_TokenizeString</summary>
        public const int MAX_STRING_CHARS       = 1024;
        
        /// <summary>Max tokens resulting from Cmd_TokenizeString</summary>
        public const int MAX_STRING_TOKENS      = 1024;

        /// <summary>Max length of an individual token</summary>
        public const int MAX_TOKEN_CHARS        = 1024;

        public const int MAX_INFO_STRING        = 1024;
        public const int MAX_INFO_KEY           = 1024;
        public const int MAX_INFO_VALUE         = 1024;

        /// <summary>Used for system info key only</summary>
        public const int BIG_INFO_STRING        = 8192;
        public const int BIG_INFO_KEY           = 8192;
        public const int BIG_INFO_VALUE         = 8192;


        // font rendering values used by ui and cgame

        public const int PROP_GAP_WIDTH             = 3;
        public const int PROP_SPACE_WIDTH           = 8;
        public const int PROP_HEIGHT			    = 27;
        public const float PROP_SMALL_SIZE_SCALE	= 0.75F;

        public const int BLINK_DIVISOR			= 200;
        public const int PULSE_DIVISOR			= 75;

        public const int UI_LEFT		    = 0x00000000;	// default
        public const int UI_CENTER		    = 0x00000001;
        public const int UI_RIGHT		    = 0x00000002;
        public const int UI_FORMATMASK	    = 0x00000007;
        public const int UI_SMALLFONT	    = 0x00000010;
        public const int UI_BIGFONT		    = 0x00000020;	// default
        public const int UI_GIANTFONT	    = 0x00000040;
        public const int UI_DROPSHADOW	    = 0x00000800;
        public const int UI_BLINK		    = 0x00001000;
        public const int UI_INVERSE		    = 0x00002000;
        public const int UI_PULSE		    = 0x00004000;

        // all drawing is done to a 640*480 virtual screen size
        // and will be automatically scaled to the real resolution
        public const int SCREEN_WIDTH		= 640;
        public const int SCREEN_HEIGHT		= 480;

        public const int TINYCHAR_WIDTH     = (SMALLCHAR_WIDTH);
        public const int TINYCHAR_HEIGHT    = (SMALLCHAR_HEIGHT / 2);

        public const int SMALLCHAR_WIDTH    = 8;
        public const int SMALLCHAR_HEIGHT	= 16;

        public const int BIGCHAR_WIDTH		= 16;
        public const int BIGCHAR_HEIGHT		= 16;

        public const int GIANTCHAR_WIDTH	= 32;
        public const int GIANTCHAR_HEIGHT	= 48;

        
        [Flags]
        /// <summary>CVar flags</summary>
        public enum CVarFlags
        {
            /// <summary>No cvar flag</summary>
            CVAR_NULL               = 0,

            /// <summary>Set to cause it to be saved to vars.rc, used for system variables, not for player specific configurations</summary>
            CVAR_ARCHIVE            = 1,

            /// <summary>Sent to server on connect or change</summary>
            CVAR_USERINFO           = 2,
            
            /// <summary>Sent in response to front end requests</summary>
            CVAR_SERVERINFO         = 4,
            
            /// <summary>These cvars will be duplicated on all clients</summary>
            CVAR_SYSTEMINFO         = 8,
            
            /// <summary>Don't allow change from console at all, but can be set from the command line</summary>
            CVAR_INIT               = 16,
            
            /// <summary>
            /// It will only change when gets the cvar, so it can't be changed
			/// without proper initialization. Modified will be set, even though the value hasn't
			/// changed yet
            /// </summary>
            CVAR_LATCH              = 32,
            
            /// <summary>Display only, cannot be set by user at all</summary>
            CVAR_ROM                = 64,

            /// <summary>Created by a set command</summary>
            CVAR_USER_CREATED       = 128,
            
            /// <summary>Can be set even when cheats are disabled, but it is not archived</summary>
            CVAR_TEMP               = 256,
            
            /// <summary>Can not be changed if cheats are disabled</summary>
            CVAR_CHEAT              = 512,
            
            /// <summary>Do not clear when a cvar_restart is issued</summary>
            CVAR_NORESTART          = 1024
        }

        public const int MAX_CVAR_VALUE_STRING  = 256;


        /// <summary>Sound channels</summary>
        public enum SoundChannel
        {
            /// <summary>Auto-select channel</summary>
            CHAN_AUTO,

            /// <summary>Menu sounds, etc</summary>
            CHAN_LOCAL,
            
            /// <summary>Weapon sounds</summary>
            CHAN_WEAPON,
            
            /// <summary>Voice communications</summary>
            CHAN_VOICE,

            /// <summary>Item pickup sounds</summary>
            CHAN_ITEM,

            /// <summary>Body impact sounds</summary>
            CHAN_BODY,
            
            /// <summary>Chat messages, etc</summary>
            CHAN_LOCAL_SOUND,
            
            /// <summary>Announcer voices, etc</summary>
            CHAN_ANNOUNCER
        }


        public const int SNAPFLAG_RATE_DELAYED      = 0;
        public const int SNAPFLAG_NOT_ACTIVE        = 1;
        public const int SNAPFLAG_SERVERCOUNT       = 2;


        public const int MAX_CLIENTS        = 64;
        public const int MAX_LOCATIONS      = 64;
        public const int GENTITYNUM_BITS    = 10;
        public const int MAX_GENTITIES      = 1 << GENTITYNUM_BITS;

        public const int ENTITYNUM_NONE             = MAX_GENTITIES - 1;
        public const int ENTITYNUM_WORLD            = MAX_GENTITIES - 2;
        public const int ENTITYNUM_MAX_NORMAL       = MAX_GENTITIES - 2;

        public const int MAX_MODELS             = 256;
        public const int MAX_SOUNDS             = 256;
        public const int MAX_CONFIGSTRINGS      = 1024;

        public const int CS_SERVERINFO                  = 0;
        public const int CS_SYSTEMINFO                  = 1;
        public const int RESERVED_CONFIGSTRINGS         = 2;

        public const int MAX_GAMESTATE_CHARS    = 16000;

        public const int MAX_STATS          = 16;
        public const int MAX_PERSISTANT     = 16;
        public const int MAX_POWERUPS       = 16;
        public const int MAX_WEAPONS        = 16;
        public const int MAX_PS_EVENTS      = 2;

        public const int PS_PMOVEFRAMECOUNTBITS         = 6;
        public const int SOLID_BMODEL                   = 0xffffff;


        /// <summary>Server Browser</summary>
        public enum ServerBrowserSource
        {
            /// <summary>Local servers</summary>
            AS_LOCAL,

            /// <summary>Multiplayer servers (rarely used)</summary>
            AS_MPLAYER,

            /// <summary>Global internet servers</summary>
            AS_GLOBAL,

            /// <summary>Favorite servers</summary>
            AS_FAVORITES
        }

        public const int MAX_GLOBAL_SERVERS             = 3072;
        public const int MAX_PINGREQUESTS               = 32;
        public const int MAX_SERVERSTATUSREQUESTS       = 16;


        public const int SAY_ALL        = 0;
        public const int SAY_TEAM       = 1;
        public const int SAY_TELL       = 2;


        public const char Q_COLOR_ESCAPE = '^';

        /// <summary>Checks if a string position contains a color code</summary>
        /// <param name="s">Input string</param>
        /// <param name="index">Position to check</param>
        /// <returns>True if valid color code sequence</returns>
        public static bool Q_IsColorString(string s, int index = 0)
        {
            // check that the position is valid and that the sequence has the color pattern
            return s != null &&
                   index >= 0 &&
                   index + 1 < s.Length &&
                   s[index] == Q_COLOR_ESCAPE &&
                   s[index + 1] != Q_COLOR_ESCAPE;
        }

        public const char COLOR_BLACK           = '0';
        public const char COLOR_RED             = '1';
        public const char COLOR_GREEN           = '2';
        public const char COLOR_YELLOW          = '3';
        public const char COLOR_BLUE            = '4';
        public const char COLOR_CYAN            = '5';
        public const char COLOR_MAGENTA         = '6';
        public const char COLOR_WHITE           = '7';

        /// <summary>Maps color character to numeric index</summary>
        /// <param name="c">Color character (0-7)</param>
        /// <returns>Color index (0-7)</returns>
        public static int ColorIndex(char c) => ((c - '0') & 7);

        public const string S_COLOR_BLACK       = "^0";
        public const string S_COLOR_RED         = "^1";
        public const string S_COLOR_GREEN       = "^2";
        public const string S_COLOR_YELLOW      = "^3";
        public const string S_COLOR_BLUE        = "^4";
        public const string S_COLOR_CYAN        = "^5";
        public const string S_COLOR_MAGENTA     = "^6";
        public const string S_COLOR_WHITE       = "^7";

        public const string S_COLOR_STRIP       = S_COLOR_WHITE;

        /// <summary>Clamps a value between min and max</summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="value">Input value</param>
        /// <returns>Clamped value</returns>
        public static float Com_Clamp(float min, float max, float value)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        /// <summary>Extracts filename from path</summary>
        /// <param name="pathname">Full path</param>
        /// <returns>Filename portion</returns>
        public static string COM_SkipPath(string pathname)
        {
            if (string.IsNullOrEmpty(pathname))
                return string.Empty;

            int lastSlash = pathname.LastIndexOf('/');
            if (lastSlash >= 0 && lastSlash < pathname.Length - 1)
                return pathname[(lastSlash + 1)..];
            else
                return pathname;
        }

        /// <summary>Removes file extension</summary>
        /// <param name="path">Filename or path</param>
        /// <returns>String without extension</returns>
        public static string COM_StripExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            int lastSlash = path.LastIndexOf('/');
            int lastDot = path.LastIndexOf('.');

            // ensure the dot is after the last slash (e.g. it's part of the file name, not a folder)
            if (lastDot > lastSlash)
            {
                return path.Substring(0, lastDot);
            }

            return path;
        }

        /// <summary>Appends extension if missing</summary>
        /// <param name="path">Filename</param>
        /// <param name="maxSize">Maximum result size</param>
        /// <param name="extension">Extension to add (.ext)</param>
        /// <returns>Path with guaranteed extension</returns>
        public static string COM_DefaultExtension(string path, int maxSize, string extension)
        {
            if (string.IsNullOrEmpty(path))
                return (path + extension).Substring(0, Math.Min(maxSize - 1, path.Length + extension.Length));

            int lastSlash = path.LastIndexOf('/');
            int lastDot = path.LastIndexOf('.');

            // if there's a dot after the last slash, it already has an extension
            if (lastDot > lastSlash)
                return path.Substring(0, Math.Min(maxSize - 1, path.Length));

            // if it doesn't have an extension, add one
            string result = path + extension;
            return result.Substring(0, Math.Min(maxSize - 1, result.Length));
        }


        /*
        ============================================================================

        PARSING

        ============================================================================
        */

        private static string com_token = "";
        private static char[] com_parsename = new char[MAX_TOKEN_CHARS];
        private static int com_lines = 1;
        private static int com_tokenline = 0;

        private static readonly bool[] is_separator = new bool[256];

        /// <summary>Initializes parsing session</summary>
        /// <param name="name">Name for error tracking</param>
        public static void COM_BeginParseSession(string name)
        {
            com_lines = 1;
            com_tokenline = 0;
            Q_strncpyz(ref com_parsename, name, MAX_TOKEN_CHARS);
        }

        /// <summary>Gets current line number during parsing</summary>
        /// <returns>Current line number</returns>
        public static int COM_GetCurrentParseLine()
        {
            return com_tokenline != 0 ? com_tokenline : com_lines;
        }

        /// <summary>Parses next token from string</summary>
        /// <param name="data">String to parse (advanced by reference)</param>
        /// <returns>Next token</returns>
        public static string COM_Parse(ref string data)
        {
            return COM_ParseExt(ref data!, true);
        }

        /// <summary>Outputs parse error with context</summary>
        /// <param name="format">Error message format</param>
        /// <param name="args">Format arguments</param>
        public static void COM_ParseError(string format, params object[] args)
        {
            StringBuilder sb = new StringBuilder(4096);
            QLib.Q_vsprintf(sb, format, args);
            Com_Printf("ERROR: {0}, line {1}: {2}\n", com_parsename, com_lines, sb.ToString());
        }

        /// <summary>
        /// Outputs a parse warning with current context information
        /// </summary>
        /// <param name="format">Format string for warning message</param>
        /// <param name="args">Arguments for the format string</param>
        public static void COM_ParseWarning(string format, params object[] args)
        {
            StringBuilder sb = new StringBuilder(4096);
            QLib.Q_vsprintf(sb, format, args);
            Com_Printf("WARNING: {0}, line {1}: {2}\n", com_parsename, com_lines, sb.ToString());
        }

        private static string? SkipWhitespace(string data, out bool hasNewLines)
        {
            hasNewLines = false;
            int i = 0;

            while (i < data.Length && data[i] <= ' ')
            {
                if (data[i] == '\0')
                    return null;

                if (data[i] == '\n')
                {
                    com_lines++;
                    hasNewLines = true;
                }

                i++;
            }

            if (i >= data.Length)
                return null;

            return data.Substring(0, i);
        }

        /// <summary>Compresses text by removing comments/whitespace</summary>
        /// <param name="data">Text to compress (modified in place)</param>
        /// <returns>Length of compressed text</returns>
        public static int COM_Compress(ref string data)
        {
            if (string.IsNullOrEmpty(data))
                return 0;

            StringBuilder outBuilder = new StringBuilder(data.Length);
            int i = 0;
            bool newline = false, whitespace = false;

            while (i < data.Length)
            {
                char c = data[i];

                // skip "//" comments
                if (c == '/' && (i + 1 < data.Length && data[i + 1] == '/'))
                {
                    i += 2;
                    while (i < data.Length && data[i] != '\n')
                        i++;
                }
                // skip "/* */" comments
                else if (c == '/' && (i + 1 < data.Length && data[i + 1] == '*'))
                {
                    i += 2;
                    while (i + 1 < data.Length && !(data[i] == '*' && data[i + 1] == '/'))
                        i++;
                    if (i + 1 < data.Length)
                        i += 2;
                }
                else if (c == '\n' || c == '\r')
                {
                    newline = true;
                    i++;
                }
                else if (c == ' ' || c == '\t')
                {
                    whitespace = true;
                    i++;
                }
                else
                {
                    // emit newline or whitespace if pending
                    if (newline)
                    {
                        outBuilder.Append('\n');
                        newline = false;
                        whitespace = false;
                    }
                    else if (whitespace)
                    {
                        outBuilder.Append(' ');
                        whitespace = false;
                    }

                    // quoted string: copy it whole
                    if (c == '"')
                    {
                        outBuilder.Append(c);
                        i++;
                        while (i < data.Length && data[i] != '"')
                        {
                            outBuilder.Append(data[i]);
                            i++;
                        }
                        if (i < data.Length && data[i] == '"')
                        {
                            outBuilder.Append('"');
                            i++;
                        }
                    }
                    else
                    {
                        outBuilder.Append(c);
                        i++;
                    }
                }
            }

            data = outBuilder.ToString();
            return data.Length;
        }

        /// <summary>Parses token with extended options</summary>
        /// <param name="data">String to parse</param>
        /// <param name="allowLineBreaks">Whether to allow newline tokens</param>
        /// <returns>Parsed token</returns>
        public static string COM_ParseExt(ref string data, bool allowLineBreaks)
        {
            bool hasNewLines;
            com_token = string.Empty;
            com_tokenline = 0;

            if (string.IsNullOrEmpty(data))
            {
                data = null!;
                return com_token;
            }

            while (true)
            {
                // skip whitespace
                data = SkipWhitespace(data!, out hasNewLines)!;
                if (data == null)
                    return com_token;

                if (hasNewLines && !allowLineBreaks)
                    return com_token;

                if (data.StartsWith("//"))
                {
                    // Skip single-line comment
                    int newlineIndex = data.IndexOf('\n');
                    if (newlineIndex == -1)
                    {
                        data = null!;
                        return com_token;
                    }
                    data = data!.Substring(0, (newlineIndex + 1));
                }
                else if (data.StartsWith("/*"))
                {
                    // skip multi-line comment
                    int end = data.IndexOf("*/");
                    while (end == -1 && data != null)
                    {
                        if (data.Contains('\n'))
                            com_lines++;
                        data = data.Substring(0, 1);
                        end = data.IndexOf("*/");
                    }

                    data = null!;
                    if (end >= 0)
                        data = data!.Substring(0, (end + 2));
                }
                else
                {
                    break;
                }
            }

            com_tokenline = com_lines;

            // handle quoted strings
            if (data.StartsWith("\""))
            {
                StringBuilder tokenBuilder = new StringBuilder();
                data = data.Substring(0, 1); // skip initial quote

                while (data.Length > 0)
                {
                    char c = data[0];
                    if (c == '"' || c == '\0')
                    {
                        if (c == '"')
                            data = data.Substring(0, 1);
                        com_token = tokenBuilder.ToString();
                        return com_token;
                    }

                    if (c == '\n')
                        com_lines++;

                    if (tokenBuilder.Length < MAX_TOKEN_CHARS - 1)
                        tokenBuilder.Append(c);

                    data = data.Substring(0, 1);
                }

                com_token = tokenBuilder.ToString();
                return com_token;
            }

            // parse a regular word
            {
                StringBuilder tokenBuilder = new StringBuilder();

                while (data.Length > 0 && data[0] > ' ')
                {
                    if (tokenBuilder.Length < MAX_TOKEN_CHARS - 1)
                        tokenBuilder.Append(data[0]);

                    data = data.Substring(0, 1);
                }

                com_token = tokenBuilder.ToString();
                return com_token;
            }
        }

        /// <summary>Requires specific token during parsing</summary>
        /// <param name="buf">Input buffer</param>
        /// <param name="match">Token to match</param>
        public static void COM_MatchToken(ref string buf, string match)
        {
            string token = COM_Parse(ref buf);
            if (!string.Equals(token, match, StringComparison.Ordinal))
            {
                Com_Error(ERR_DROP, "MatchToken: {0} != {1}", token, match);
            }
        }

        /// <summary>Skips braced section { ... }</summary>
        /// <param name="program">Input buffer</param>
        public static void SkipBracedSection(ref string program)
        {
            int depth = 0;
            string token;

            do
            {
                token = COM_ParseExt(ref program!, true);
                if (token.Length == 1)
                {
                    if (token[0] == '{')
                        depth++;
                    else if (token[0] == '}')
                        depth--;
                }
            } while (depth > 0 && program != null && program.Length > 0);
        }

        /// <summary>Skips rest of current line</summary>
        /// <param name="data">Input buffer</param>
        public static void SkipRestOfLine(ref string data)
        {
            if (string.IsNullOrEmpty(data))
                return;

            int index = 0;
            while (index < data.Length)
            {
                if (data[index] == '\n')
                {
                    com_lines++;
                    index++;
                    break;
                }
                index++;
            }

            data = index < data.Length ? data.Substring(index) : string.Empty;
        }

        /// <summary>
        /// Initializes the separator character table
        /// </summary>
        public static void Com_InitSeparators()
        {
            is_separator['\n']  = true;
            is_separator[';']   = true;
            is_separator['=']   = true;
            is_separator['{']   = true;
            is_separator['}']   = true;
        }

        /// <summary>
        /// Advances data pointer until a separator character is found
        /// </summary>
        /// <param name="data">Input string (modified by reference)</param>
        public static void SkipTillSeparators(ref string? data)
        {
            if (string.IsNullOrEmpty(data))
                return;

            int index = 0;
            while (index < data.Length)
            {
                char c = data[index];
                index++;
                if (is_separator[c])
                {
                    if (c == '\n')
                    {
                        com_lines++;
                    }
                    break;
                }
            }

            data = index < data.Length ? data.Substring(index) : string.Empty;
        }

        /// <summary>Parses token using separator characters</summary>
        /// <param name="data">Input buffer</param>
        /// <param name="allowLineBreaks">Allow newline tokens</param>
        /// <returns>Parsed token</returns>
        public static string COM_ParseSep(ref string data, bool allowLineBreaks)
        {
            int c = 0, len = 0;
            bool hasNewLines = false;
            com_tokenline = 0;
            com_token = "";

            if (string.IsNullOrEmpty(data))
            {
                data = null!;
                return string.Empty;
            }

            while (true)
            {
                data = SkipWhitespace(data, out hasNewLines)!;

                if (data == null)
                {
                    return string.Empty;
                }

                if (hasNewLines && !allowLineBreaks)
                {
                    return string.Empty;
                }

                c = data[0];

                // skip double-slash comments
                if (c == '/' && data.Length > 1 && data[1] == '/')
                {
                    int newlineIndex = data.IndexOf('\n');
                    data = newlineIndex >= 0 ? data.Substring(newlineIndex + 1) : null!;
                }
                // skip /* */ comments
                else if (c == '/' && data.Length > 1 && data[1] == '*')
                {
                    int endComment = data.IndexOf("*/");
                    int newlines = data.Take(endComment >= 0 ? endComment : data.Length).Count(ch => ch == '\n');
                    com_lines += newlines;
                    data = endComment >= 0 ? data.Substring(endComment + 2) : null!;
                }
                else
                {
                    break;
                }

                if (data == null)
                {
                    return string.Empty;
                }
            }

            com_tokenline = com_lines;

            // quoted string
            if (c == '"')
            {
                data = data.Substring(1);
                while (true)
                {
                    if (string.IsNullOrEmpty(data))
                        break;

                    c = data[0];
                    if (c == '"' || c == '\0')
                    {
                        if (c == '"')
                            data = data.Length > 1 ? data.Substring(1) : string.Empty;

                        return com_token;
                    }

                    if (c == '\n')
                    {
                        com_lines++;
                    }

                    if (len < MAX_TOKEN_CHARS - 1)
                    {
                        com_token = c.ToString();
                    }

                    data = data.Length > 1 ? data.Substring(1) : string.Empty;
                }
            }

            // separator token
            if (is_separator[c])
            {
                com_token = c.ToString();
                data = data.Length > 1 ? data.Substring(1) : string.Empty;
            }
            else
            {
                // regular word
                while (c > ' ' && !is_separator[c])
                {
                    if (len < MAX_TOKEN_CHARS - 1)
                    {
                        com_token = c.ToString();
                    }

                    data = data.Length > 1 ? data.Substring(1) : string.Empty;
                    if (string.IsNullOrEmpty(data))
                        break;

                    c = data[0];
                }
            }

            return com_token;
        }

        /// <summary>
        /// Splits a character buffer into tokens using a delimiter
        /// </summary>
        /// <param name="inputBuffer">Character buffer to split</param>
        /// <param name="inputLength">Length of valid data in buffer</param>
        /// <param name="output">Array to store resulting tokens</param>
        /// <param name="outputSize">Maximum number of output tokens</param>
        /// <param name="delim">Delimiter character</param>
        /// <returns>Number of tokens produced</returns>
        public static int Com_Split(char[] inputBuffer, int inputLength, string[] output, int outputSize, char delim)
        {
            int inputIndex = 0;
            int c;
            int outputIndex = 0;

            // skip leading spaces if delimiter is printable
            if (delim >= ' ')
            {
                while (inputIndex < inputLength && inputBuffer[inputIndex] <= ' ')
                {
                    inputIndex++;
                }
            }

            output[outputIndex++] = new string(inputBuffer, inputIndex, inputLength - inputIndex); // set first pointer

            while (outputIndex < outputSize)
            {
                // find delimiter
                while (inputIndex < inputLength && (c = inputBuffer[inputIndex]) != '\0' && c != delim)
                {
                    inputIndex++;
                }

                // null-terminate the current token
                if (inputIndex < inputLength)
                {
                    inputBuffer[inputIndex] = '\0';
                }

                if (inputIndex >= inputLength || inputBuffer[inputIndex] == '\0')
                {
                    // if last value was empty, don't count it
                    if (output[outputIndex - 1].Length == 0)
                    {
                        outputIndex--;
                    }
                    break;
                }

                inputIndex++; // skip the delimiter

                // skip leading whitespace after delimiter
                if (delim >= ' ')
                {
                    while (inputIndex < inputLength && inputBuffer[inputIndex] <= ' ')
                    {
                        inputIndex++;
                    }
                }

                if (inputIndex >= inputLength)
                    break;

                output[outputIndex++] = new string(inputBuffer, inputIndex, inputLength - inputIndex);
            }

            // sanitize last token (terminate at next delimiter or null)
            while (inputIndex < inputLength && inputBuffer[inputIndex] != '\0' && inputBuffer[inputIndex] != delim)
            {
                inputIndex++;
            }
            if (inputIndex < inputLength)
            {
                inputBuffer[inputIndex] = '\0';
            }

            // fill unused output slots with last pointer
            while (outputIndex < outputSize)
            {
                output[outputIndex++] = new string(inputBuffer, inputIndex, 0);
            }

            return outputIndex;
        }


        /*
        ============================================================================

					        LIBRARY REPLACEMENT FUNCTIONS

        ============================================================================
        */

        public static readonly byte[] locase = new byte[256]
        {
            0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,
            0x08,0x09,0x0a,0x0b,0x0c,0x0d,0x0e,0x0f,
            0x10,0x11,0x12,0x13,0x14,0x15,0x16,0x17,
            0x18,0x19,0x1a,0x1b,0x1c,0x1d,0x1e,0x1f,
            0x20,0x21,0x22,0x23,0x24,0x25,0x26,0x27,
            0x28,0x29,0x2a,0x2b,0x2c,0x2d,0x2e,0x2f,
            0x30,0x31,0x32,0x33,0x34,0x35,0x36,0x37,
            0x38,0x39,0x3a,0x3b,0x3c,0x3d,0x3e,0x3f,
            0x40,0x61,0x62,0x63,0x64,0x65,0x66,0x67,
            0x68,0x69,0x6a,0x6b,0x6c,0x6d,0x6e,0x6f,
            0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,
            0x78,0x79,0x7a,0x5b,0x5c,0x5d,0x5e,0x5f,
            0x60,0x61,0x62,0x63,0x64,0x65,0x66,0x67,
            0x68,0x69,0x6a,0x6b,0x6c,0x6d,0x6e,0x6f,
            0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,
            0x78,0x79,0x7a,0x7b,0x7c,0x7d,0x7e,0x7f,
            0x80,0x81,0x82,0x83,0x84,0x85,0x86,0x87,
            0x88,0x89,0x8a,0x8b,0x8c,0x8d,0x8e,0x8f,
            0x90,0x91,0x92,0x93,0x94,0x95,0x96,0x97,
            0x98,0x99,0x9a,0x9b,0x9c,0x9d,0x9e,0x9f,
            0xa0,0xa1,0xa2,0xa3,0xa4,0xa5,0xa6,0xa7,
            0xa8,0xa9,0xaa,0xab,0xac,0xad,0xae,0xaf,
            0xb0,0xb1,0xb2,0xb3,0xb4,0xb5,0xb6,0xb7,
            0xb8,0xb9,0xba,0xbb,0xbc,0xbd,0xbe,0xbf,
            0xc0,0xc1,0xc2,0xc3,0xc4,0xc5,0xc6,0xc7,
            0xc8,0xc9,0xca,0xcb,0xcc,0xcd,0xce,0xcf,
            0xd0,0xd1,0xd2,0xd3,0xd4,0xd5,0xd6,0xd7,
            0xd8,0xd9,0xda,0xdb,0xdc,0xdd,0xde,0xdf,
            0xe0,0xe1,0xe2,0xe3,0xe4,0xe5,0xe6,0xe7,
            0xe8,0xe9,0xea,0xeb,0xec,0xed,0xee,0xef,
            0xf0,0xf1,0xf2,0xf3,0xf4,0xf5,0xf6,0xf7,
            0xf8,0xf9,0xfa,0xfb,0xfc,0xfd,0xfe,0xff
        };

        /// <summary>Checks if character is printable</summary>
        /// <param name="c">Character code</param>
        /// <returns>True if printable ASCII</returns>
        public static bool Q_isprint(int c) => c >= 0x20 && c <= 0x7E;

        /// <summary>Checks if character is lowercase letter</summary>
        /// <param name="c">Character code</param>
        /// <returns>True if lowercase letter</returns>
        public static bool Q_islower(int c) => c >= 'a' && c <= 'z';

        /// <summary>
        /// Checks if a character is an uppercase letter
        /// </summary>
        /// <param name="c">Character code to check</param>
        /// <returns>True if character is between 'A' and 'Z'</returns>
        public static bool Q_isupper(int c) => c >= 'A' && c <= 'Z';

        /// <summary>Checks if character is alphabetic</summary>
        /// <param name="c">Character code</param>
        /// <returns>True if letter</returns>
        public static bool Q_isalpha(int c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');

        /// <summary>Finds last occurrence of character in string</summary>
        /// <param name="input">Input string</param>
        /// <param name="c">Character to find</param>
        /// <returns>Last index or -1</returns>
        public static int Q_strrchr(string input, char c) => (input == null) ? -1 : input.LastIndexOf(c);



        /// <summary>Case-insensitive string comparison (limited length)</summary>
        /// <param name="s1">First string</param>
        /// <param name="s2">Second string</param>
        /// <param name="n">Maximum characters to compare</param>
        /// <returns>Comparison result (-1, 0, 1)</returns>
        public static int Q_stricmpn(string s1, string s2, int n)
        {
            if (s1 == null) return s2 == null ? 0 : -1;
            if (s2 == null) return 1;

            int len1 = Math.Min(s1.Length, n);
            int len2 = Math.Min(s2.Length, n);
            int minLen = Math.Min(len1, len2);

            int cmp = string.Compare(s1.Substring(0, minLen), s2.Substring(0, minLen), StringComparison.OrdinalIgnoreCase);
            if (cmp != 0)
                return cmp;

            // handle partial comparison when strings are shorter than n
            if (len1 < len2) return -1;
            if (len1 > len2) return 1;

            return 0;
        }

        /// <summary>Case-sensitive string comparison (limited length)</summary>
        /// <param name="s1">First string</param>
        /// <param name="s2">Second string</param>
        /// <param name="n">Maximum characters to compare</param>
        /// <returns>Comparison result (-1, 0, 1)</returns>
        public static int Q_strncmp(string s1, string s2, int n)
        {
            if (s1 == null) return s2 == null ? 0 : -1;
            if (s2 == null) return 1;

            int len1 = Math.Min(s1.Length, n);
            int len2 = Math.Min(s2.Length, n);
            int minLen = Math.Min(len1, len2);

            int cmp = string.CompareOrdinal(s1.Substring(0, minLen), s2.Substring(0, minLen));
            if (cmp != 0)
                return cmp;

            // if substrings equal but one string is shorter than n
            if (len1 < len2) return -1;
            if (len1 > len2) return 1;

            return 0;
        }

        /// <summary>Case-insensitive string comparison</summary>
        /// <param name="s1">First string</param>
        /// <param name="s2">Second string</param>
        /// <returns>Comparison result (-1, 0, 1)</returns>
        public static int Q_stricmp(string s1, string s2)
        {
            if (s1 == null) return s2 == null ? 0 : -1;
            if (s2 == null) return 1;

            return string.Compare(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Safe string copy with guaranteed null termination</summary>
        /// <param name="dest">Destination buffer</param>
        /// <param name="src">Source string</param>
        /// <param name="destSize">Destination buffer size</param>
        public static void Q_strncpyz(ref char[] dest, string src, int destSize)
        {
            if (dest == null)
                Com_Error(ERR_FATAL, new ArgumentException("Q_strncpyz: NULL dest").Message);

            if (src == null)
                Com_Error(ERR_FATAL, new ArgumentException("Q_strncpyz: NULL src").Message);

            if (destSize < 1)
                Com_Error(ERR_FATAL, new ArgumentException("Q_strncpyz: destsize < 1", nameof(destSize)).Message);

            int len = Math.Min(src!.Length, destSize - 1);

            for (int i = 0; i < len; i++)
            {
                dest![i] = src[i];
            }

            // ensure null termination (equivalent to '\0')
            dest![len] = '\0';
        }


        /// <summary>Converts string to lowercase</summary>
        /// <param name="s1">Input string</param>
        /// <returns>Lowercase string</returns>
        public static string Q_strlwr(string s1) => s1!.ToLowerInvariant();

        /// <summary>
        /// Converts a string to uppercase using invariant culture
        /// </summary>
        /// <param name="s1">Input string</param>
        /// <returns>Uppercase version of input string</returns>
        public static string Q_strupr(string s1) => s1!.ToUpperInvariant();

        /// <summary>
        /// Safely concatenates a string to a character array with guaranteed null termination
        /// </summary>
        /// <param name="dest">Destination character array (modified by reference)</param>
        /// <param name="size">Total size of destination buffer</param>
        /// <param name="src">Source string to append</param>
        // never goes past bounds or leaves without a terminating 0
        public static void Q_strcat(ref char[] dest, int size, string src)
        {
            if (dest == null || src == null)
                Com_Error(ERR_FATAL, new ArgumentNullException().Message);

            int l1 = 0;

            // find the length of the current string in destination
            while (l1 < size && dest![l1] != '\0')
                l1++;

            if (l1 >= size)
                Com_Error(ERR_FATAL, new InvalidOperationException("Q_strcat: already overflowed").Message);

            int remaining = size - l1;

            // copy src into dest from destLen
            int i = 0;
            while (i < src!.Length && i < remaining - 1)
            {
                dest![l1 + i] = src[i];
                i++;
            }

            // ensure null termination
            if (l1 + i < size)
                dest![l1 + i] = '\0';
        }


        /// <summary>Calculates visible string length (ignores color codes)</summary>
        /// <param name="str">Input string</param>
        /// <returns>Visible character count</returns>
        public static int Q_PrintStrlen(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Q_IsColorString(str, i))
                {
                    i++; // skip color code (two chars)
                    continue;
                }
                len++;
            }
            return len;
        }

        /// <summary>Removes color codes and non-printable characters</summary>
        /// <param name="str">Input string</param>
        /// <returns>Cleaned string</returns>
        public static string Q_CleanStr(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var sb = new System.Text.StringBuilder(str.Length);

            for (int i = 0; i < str.Length; i++)
            {
                if (Q_IsColorString(str, i))
                {
                    i++; // skip color code
                }
                else
                {
                    char c = str[i];
                    if (c >= ' ' && c <= '~')
                    {
                        sb.Append(c);
                    }
                }
            }

            return sb.ToString();
        }


        /// <summary>Prints formatted message to console</summary>
        /// <param name="fmt">Format string</param>
        /// <param name="args">Format arguments</param>
        public static void Com_Printf(string fmt, params object[] args)
        {
            Console.WriteLine(string.Format(fmt, args));
        }

        /// <summary>Throws formatted error exception</summary>
        /// <param name="level">Error severity level</param>
        /// <param name="fmt">Format string</param>
        /// <param name="args">Format arguments</param>
        public static void Com_Error(int level, string fmt, params object[] args)
        {
            throw new Exception(string.Format(fmt, args));
        }

        public const int ERR_FATAL                  = 1;        // exit the entire game with a popup window
        public const int ERR_DROP                   = 2;        // print to console and disconnect from game
        public const int ERR_SERVERDISCONNECT       = 3;        // don't kill server
        public const int ERR_DISCONNECT             = 4;        // client disconnected from the server
        public const int ERR_NEED_CD                = 5;        // pop up the need-cd dialog

        /// <summary>Formatted string output with bounds checking</summary>
        /// <param name="dest">Output string</param>
        /// <param name="size">Maximum output size</param>
        /// <param name="fmt">Format string</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Length of formatted string</returns>
        public static int Com_sprintf(out string dest, int size, string fmt, params object[] args)
        {
            StringBuilder sb = new StringBuilder(4096);
            int len = QLib.Q_vsprintf(sb, fmt, args);
            string result = sb.ToString();

            if (len >= size)
                Com_Error(ERR_FATAL, new InvalidOperationException($"Com_sprintf: overflow of {len} in {size}").Message);

            // truncate if it exceeds the allowed size
            dest = result.Length < size ? result : result.Substring(0, size);
            return len;
        }


        /*
        =====================================================================

          INFO STRINGS

        =====================================================================
        */


        /// <summary>
        /// Compares two strings case-insensitively for a specified length
        /// </summary>
        /// <param name="str">First string to compare</param>
        /// <param name="key">Second string to compare</param>
        /// <param name="key_len">Number of characters to compare</param>
        /// <returns>True if the specified portions match case-insensitively</returns>
        public static bool Q_strkey(string str, string key, int key_len)
        {
            if (str == null || key == null || str.Length < key_len || key.Length < key_len)
                return false;

            for (int i = 0; i < key_len; i++)
            {
                if (locase[(byte)str[i]] != locase[(byte)key[i]])
                    return false;
            }
            return true;
        }

        private static readonly string[] valueBuffers = new string[2];  // use two buffers so compares work without stomping on each other
        private static int valueIndex = 0;

        /// <summary>
        /// Searches the string for the given key and returns the associated value, or an empty string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns>Associated value or empty string</returns>
        public static string Info_ValueForKey(string s, string key)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(key))
                return string.Empty;

            int klen = key.Length;
            int i = 0;

            if (s[0] == '\\')
                i++;

            while (i < s.Length)
            {
                // parse key
                int keyStart = i;
                while (i < s.Length && s[i] != '\\') i++;
                int keyLen = i - keyStart;

                if (i >= s.Length) break;
                i++; // skip '\'

                // parse value
                int valStart = i;
                while (i < s.Length && s[i] != '\\') i++;
                int valLen = i - valStart;

                if (keyLen == klen && Q_strkey(s.Substring(keyStart, keyLen), key, klen))
                {
                    if (valLen >= BIG_INFO_STRING)
                        Com_Error(ERR_DROP, new InvalidOperationException("Info_ValueForKey: oversize infostring value").Message);

                    string result = s.Substring(valStart, valLen);
                    valueBuffers[valueIndex] = result;
                    valueIndex ^= 1;
                    return result;
                }

                if (i >= s.Length) break;
                i++; // skip '\'
            }

            return string.Empty;
        }

        /// <summary>
        /// Used to itterate through all the key/value pairs in an info string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Key index</returns>
        public static int Info_NextPair(string s, out string key, out string value)
        {
            key = string.Empty;
            value = string.Empty;

            if (string.IsNullOrEmpty(s))
                return -1;

            int index = 0;

            // skip leading backslash
            if (s[index] == '\\')
                index++;

            // read key
            int keyStart = index;
            while (index < s.Length && s[index] != '\\')
            {
                index++;
            }

            if (index >= s.Length)
                return -1;

            key = s.Substring(keyStart, index - keyStart);
            index++; // skip backslash between key and value

            // read value
            int valueStart = index;
            while (index < s.Length && s[index] != '\\')
            {
                index++;
            }

            value = s.Substring(valueStart, index - valueStart);

            // return next start index or -1 if at end
            if (index >= s.Length)
                return -1;

            return index;
        }

        private static int Info_RemoveKey(ref string s, string key)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(key))
                return 0;

            int keyLen = key.Length;
            int i = 0;

            while (i < s.Length)
            {
                int start = i;

                // skip initial backslash
                if (s[i] == '\\') i++;

                // read key
                int keyStart = i;
                while (i < s.Length && s[i] != '\\') i++;
                int keySegmentLen = i - keyStart;
                if (i >= s.Length) return 0;
                string currentKey = s.Substring(keyStart, keySegmentLen);
                i++; // Skip backslash

                // read value
                int valueStart = i;
                while (i < s.Length && s[i] != '\\') i++;
                int valueEnd = i;

                // compare keys (case-insensitive)
                if (keySegmentLen == keyLen &&
                    Q_strkey(currentKey, key, keyLen))
                {
                    // remove the matched pair
                    string before = s.Substring(0, start);
                    string after = (i < s.Length) ? s.Substring(i) : "";
                    s = before + after;
                    return i - start;
                }

                if (i >= s.Length)
                    break;
            }

            return 0;
        }

        /// <summary>
        /// Some characters are illegal in info strings because they can mess up the server's parsing.
        /// </summary>
        /// <param name="s">String to validate</param>
        /// <returns>True if valid</returns>
        public static bool Info_Validate(string s)
        {
            if (s == null)
                return true;

            foreach (char c in s)
            {
                switch (c)
                {
                    case '"':
                    case ';':
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates characters in a key or value string
        /// </summary>
        /// <param name="s">String to validate</param>
        /// <returns>True if string contains only valid characters</returns>
        public static bool Info_ValidateKeyValue(string s)
        {
            if (s == null)
                return true;

            foreach (char c in s)
            {
                switch (c)
                {
                    case '\\':
                    case '"':
                    case ';':
                        return false;
                }
            }

            return true;
        }

        /// <summary>Sets or adds key/value pair</summary>
        /// <param name="info">Info string (modified)</param>
        /// <param name="key">Key to set</param>
        /// <param name="value">Value to set</param>
        /// <returns>True on success</returns>
        public static bool Info_SetValueForKey(ref string info, string key, string value)
        {
            if (info.Length >= MAX_INFO_STRING)
            {
                Com_Error(ERR_DROP, new InvalidOperationException("Info_SetValueForKey: oversize infostring").Message);
                return false;
            }

            if (!Info_ValidateKeyValue(key) || string.IsNullOrEmpty(key))
            {
                Com_Printf(S_COLOR_YELLOW + $"Invalid key name: '{key}'\n");
                return false;
            }

            if (!Info_ValidateKeyValue(value))
            {
                Com_Printf(S_COLOR_YELLOW + $"Invalid value name: '{value}'\n");
                return false;
            }

            // remove old key if it exists
            int removedLength = Info_RemoveKey(ref info, key);
            int currentLength = info.Length;

            if (string.IsNullOrEmpty(value))
                return true;

            string newPair = $@"\{key}\{value}";
            int totalLength = currentLength + newPair.Length;

            if (totalLength >= MAX_INFO_STRING)
            {
                Com_Printf(S_COLOR_YELLOW + "Info string length exceeded\n");
                return false;
            }

            info += newPair;
            return true;
        }

        /// <summary>Sets value for BIG_INFO_STRING keys</summary>
        /// <param name="info">Info string</param>
        /// <param name="key">Key to set</param>
        /// <param name="value">Value to set</param>
        /// <returns>True on success</returns>
        public static bool Info_SetValueForKey_Big(ref string info, string key, string value)
        {
            if (info.Length >= BIG_INFO_STRING)
            {
                Com_Error(ERR_DROP, new InvalidOperationException("Info_SetValueForKey: oversize infostring").Message);
                return false;
            }

            if (!Info_ValidateKeyValue(key) || string.IsNullOrEmpty(key))
            {
                Com_Printf(S_COLOR_YELLOW + $"Invalid key name: '{key}'\n");
                return false;
            }

            if (!Info_ValidateKeyValue(value))
            {
                Com_Printf(S_COLOR_YELLOW + $"Invalid value name: '{value}'\n");
                return false;
            }

            // remove the existing key if present
            int removedLength = Info_RemoveKey(ref info, key);
            int currentLength = info.Length;

            if (string.IsNullOrEmpty(value))
                return true;

            string newPair = $@"\{key}\{value}";
            int totalLength = currentLength + newPair.Length;

            if (totalLength >= BIG_INFO_STRING)
            {
                Com_Printf(S_COLOR_YELLOW + "BIG Info string length exceeded\n");
                return false;
            }

            info += newPair;
            return true;
        }
    }
}
