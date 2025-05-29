using System.Globalization;
using System.Text;

namespace Q3CSharpLib
{
    public static class QLib
    {
        private const int ALT           = 0x00000001;		/* alternate form */
        private const int HEXPREFIX     = 0x00000002;		/* add 0x or 0X prefix */
        private const int LADJUST       = 0x00000004;		/* left adjustment */
        private const int LONGDBL       = 0x00000008;		/* long double */
        private const int LONGINT       = 0x00000010;		/* long integer */
        private const int QUADINT       = 0x00000020;		/* quad integer */
        private const int SHORTINT      = 0x00000040;		/* short integer */
        private const int ZEROPAD       = 0x00000080;		/* zero (as opposed to blank) pad */
        private const int FPT           = 0x00000100;		/* floating point number */
        private const int REDUCE        = 0x00000200;		/* extension: do not emit anything if value is zero */

        private static bool IsDigit(char c) => c >= '0' && c <= '9';

        private static void AddInt(ref StringBuilder sb, int val, int width, int flags)
        {
            if ((flags & REDUCE) != 0 && val == 0)
                return;

            string text = Math.Abs(val).ToString();
            if (val < 0)
                text = "-" + text;

            if ((flags & LADJUST) == 0)
            {
                while (text.Length < width)
                {
                    sb.Append((flags & ZEROPAD) != 0 ? '0' : ' ');
                    width--;
                }
            }

            sb.Append(text);

            if ((flags & LADJUST) != 0)
            {
                while (text.Length < width)
                {
                    sb.Append((flags & ZEROPAD) != 0 ? '0' : ' ');
                    width--;
                }
            }
        }

        private static void AddFloat(ref StringBuilder sb, float fval, int width, int prec, bool reduce)
        {
            if (reduce && fval == 0.0f)
                return;

            if (prec < 0)
                prec = 6;

            string text = fval.ToString("F" + prec, CultureInfo.InvariantCulture);
            while (text.Length < width)
            {
                sb.Append(' ');
                width--;
            }

            sb.Append(text);
        }

        private static void AddString(ref StringBuilder sb, string str, int width, int prec)
        {
            if (str == null)
                str = "(null)";

            if (prec >= 0 && prec < str.Length)
                str = str.Substring(0, prec);

            sb.Append(str);

            while (str.Length < width)
            {
                sb.Append(' ');
                width--;
            }
        }

        /// <summary>
        /// Quake-style string formatting implementation similar to vsprintf
        /// </summary>
        /// <param name="sb">StringBuilder to append formatted output</param>
        /// <param name="fmt">Format string containing % directives</param>
        /// <param name="args">Arguments to insert into format string</param>
        /// <returns>Total length of appended string</returns>
        public static int Q_vsprintf(StringBuilder sb, string fmt, object[] args)
        {
            int argIndex = 0;
            for (int i = 0; i < fmt.Length; i++)
            {
                char ch = fmt[i];
                if (ch != '%')
                {
                    sb.Append(ch);
                    continue;
                }

                i++;
                if (i >= fmt.Length)
                    break;

                int flags = 0, width = 0, prec = -1;
                bool parsing = true;
                while (parsing && i < fmt.Length)
                {
                    ch = fmt[i];
                    switch (ch)
                    {
                        case '-': flags |= LADJUST; i++; break;
                        case '0': flags |= ZEROPAD; i++; break;
                        case '.':
                            i++;
                            if (i < fmt.Length && fmt[i] == '*')
                            {
                                prec = Convert.ToInt32(args[argIndex++]);
                                i++;
                            }
                            else
                            {
                                prec = 0;
                                while (i < fmt.Length && IsDigit(fmt[i]))
                                    prec = 10 * prec + (fmt[i++] - '0');
                            }
                            break;
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            width = 0;
                            while (i < fmt.Length && IsDigit(fmt[i]))
                                width = 10 * width + (fmt[i++] - '0');
                            break;
                        case '*':
                            width = Convert.ToInt32(args[argIndex++]);
                            i++;
                            break;
                        case 'R':
                            flags |= REDUCE;
                            i++;
                            break;
                        default:
                            parsing = false;
                            break;
                    }
                }

                if (i >= fmt.Length) break;
                ch = fmt[i];

                switch (ch)
                {
                    case 'd':
                    case 'i':
                        AddInt(ref sb, Convert.ToInt32(args[argIndex++]), width, flags);
                        break;
                    case 'f':
                        AddFloat(ref sb, Convert.ToSingle(args[argIndex++]), width, prec, (flags & REDUCE) != 0);
                        break;
                    case 's':
                        AddString(ref sb, Convert.ToString(args[argIndex++])!, width, prec);
                        break;
                    case 'c':
                        sb.Append(Convert.ToChar(args[argIndex++]));
                        break;
                    case '%':
                        sb.Append('%');
                        break;
                    default:
                        sb.Append(args[argIndex++].ToString());
                        break;
                }
            }

            return sb.Length;
        }

        /// <summary>
        /// Removes Quake color codes from a string
        /// </summary>
        /// <param name="input">String containing color codes</param>
        /// <returns>String with color codes removed</returns>
        public static string BG_StripColor(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder sb = new StringBuilder(input.Length);
            int i = 0;
            while (i < input.Length)
            {
                if (QShared.Q_IsColorString(input, i))
                {
                    // skip the escape char and color code
                    i += 2;
                }
                else
                {
                    sb.Append(input[i]);
                    i++;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Encodes special characters for network transmission
        /// </summary>
        /// <param name="input">String to encode</param>
        /// <returns>Encoded string</returns>
        public static string EncodedString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            const string hextab = "0123456789abcdef";
            StringBuilder sb = new StringBuilder(input.Length * 3); // maximum possible expansion

            foreach (char c in input)
            {
                if (c == '#')
                {
                    sb.Append("##");
                }
                else if (c > 127 || c == '%')
                {
                    sb.Append('#');
                    sb.Append(hextab[(c & 0xF0) >> 4]);
                    sb.Append(hextab[c & 0x0F]);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private static int Hex2Dec(char chr)
        {
            switch (chr)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'a': return 10;
                case 'A': return 10;
                case 'b': return 11;
                case 'B': return 11;
                case 'c': return 12;
                case 'C': return 12;
                case 'd': return 13;
                case 'D': return 13;
                case 'e': return 14;
                case 'E': return 14;
                case 'f': return 15;
                case 'F': return 15;
            }
            return 0;
        }

        /// <summary>
        /// Decodes strings encoded with EncodedString
        /// </summary>
        /// <param name="input">Encoded string</param>
        /// <returns>Decoded string</returns>
        public static string DecodedString(string input)
        {
            if (input == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder(input.Length);

            int i = 0;
            while (i < input.Length)
            {
                if (input[i] == '#' && i + 2 < input.Length)
                {
                    char c1 = input[i + 1];
                    char c2 = input[i + 2];

                    bool validHexC1 = (c1 >= '0' && c1 <= '9') || (c1 >= 'a' && c1 <= 'f') || (c1 >= 'A' && c1 <= 'F');
                    bool validHexC2 = (c2 >= '0' && c2 <= '9') || (c2 >= 'a' && c2 <= 'f') || (c2 >= 'A' && c2 <= 'F');

                    if (validHexC1 && validHexC2)
                    {
                        int val = Hex2Dec(c1) * 16 + Hex2Dec(c2);
                        sb.Append((char)val);
                        i += 3;
                        continue;
                    }
                    else if (i + 1 < input.Length && input[i + 1] == '#')
                    {
                        sb.Append('#');
                        i += 2;
                        continue;
                    }
                }

                sb.Append(input[i]);
                i++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Case-insensitive string comparison using Quake's locase table
        /// </summary>
        /// <param name="s1">First string to compare</param>
        /// <param name="s2">Second string to compare</param>
        /// <returns>
        ///   -1 if s1 < s2
        ///    0 if s1 == s2
        ///    1 if s1 > s2
        /// </returns>
        public static int BG_stricmp(string s1, string s2)
        {
            if (s1 == null) return s2 == null ? 0 : -1;
            if (s2 == null) return 1;

            int i = 0;
            while (true)
            {
                // if reaches the end of either, then it's done
                if (i >= s1.Length && i >= s2.Length) return 0;
                if (i >= s1.Length) return -1;
                if (i >= s2.Length) return 1;

                byte c1 = QShared.locase[(byte)s1[i]];
                byte c2 = QShared.locase[(byte)s2[i]];

                if (c1 != c2)
                    return c1 < c2 ? -1 : 1;

                i++;
            }
        }

        /// <summary>
        /// Case-insensitive substring search
        /// </summary>
        /// <param name="str1">String to search within</param>
        /// <param name="str2">Substring to find</param>
        /// <returns>
        /// Substring of str1 starting from first match of str2, 
        /// or null if not found
        /// </returns>
        public static string? Q_stristr(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str2))
                return str1;

            for (int i = 0; i <= str1.Length - str2.Length; i++)
            {
                int j = 0;
                while (j < str2.Length &&
                       QShared.locase[(byte)str1[i + j]] == QShared.locase[(byte)str2[j]])
                {
                    j++;
                }

                if (j == str2.Length)
                    return str1.Substring(i);
            }

            return null;
        }

        /// <summary>
        /// Sanitizes player names according to Quake rules
        /// </summary>
        /// <param name="input">Raw name input</param>
        /// <param name="output">Cleaned name output</param>
        /// <param name="outSize">Maximum output size (including null terminator)</param>
        /// <param name="blankString">Fallback name if result is empty</param>
        public static void BG_CleanName(string input, StringBuilder output, int outSize, string? blankString)
        {
            int len = 0;
            int colorlessLen = 0;
            int spaces = 0;

            output.Clear();
            outSize--; // reserve space for the null terminator

            int i = 0;
            while (i < input.Length)
            {
                char ch = input[i++];

                // don't allow spaces at the beginning
                if (output.Length == 0 && ch <= ' ')
                {
                    continue;
                }

                // check colors
                if (ch == QShared.Q_COLOR_ESCAPE)
                {
                    // if there's no next character, end
                    if (i >= input.Length)
                        break;

                    // don't allow black color
                    if (QShared.ColorIndex(input[i]) == 0)
                    {
                        i++; // skip black color
                        continue;
                    }

                    // check space for two characters
                    if (len > outSize - 2)
                        break;

                    output.Append(ch);
                    output.Append(input[i++]);
                    len += 2;
                    continue;
                }

                // printable characters only
                if (ch < ' ' || ch > 126)
                {
                    continue;
                }

                // don't allow more than 2 consecutive spaces
                if (ch == ' ')
                {
                    spaces++;
                    if (spaces > 2)
                        continue;
                }
                else
                {
                    spaces = 0;
                }

                if (len > outSize - 1)
                    break;

                output.Append(ch);
                colorlessLen++;
                len++;
            }

            // if empty or without colorless characters, put blankString
            if (!string.IsNullOrEmpty(blankString))
            {
                if (output.Length == 0 || colorlessLen == 0)
                {
                    output.Clear();
                    output.Append(blankString.Length > outSize ? blankString.Substring(0, outSize) : blankString);
                }
            }
        }

        /// <summary>
        /// Replaces all occurrences of a substring with another
        /// </summary>
        /// <param name="str1">Substring to replace</param>
        /// <param name="str2">Replacement string</param>
        /// <param name="src">Source text (modified in place)</param>
        /// <param name="maxLen">Maximum buffer size</param>
        /// <returns>Number of replacements performed</returns>
        public static int ReplaceS(string str1, string str2, StringBuilder src, int maxLen)
        {
            int count = 0;
            int len1 = str1.Length;
            int len2 = str2.Length;
            int d = len2 - len1;

            int index = src.ToString().IndexOf(str1, StringComparison.Ordinal);
            if (index == -1)
                return count;

            if (d > 0) // expand and replace mode
            {
                // it'll do replacements until no more matches
                while (index != -1)
                {
                    // check if expanding will exceed max length
                    if (src.Length + d > maxLen)
                        return count;

                    // insert extra space
                    src.Insert(index + len1, new string('\0', d)); // insert dummy chars to expand
                                                                   // shift existing chars right by d positions manually (handled by Insert)

                    // replace str1 by str2
                    for (int i = 0; i < len2; i++)
                    {
                        src[index + i] = str2[i];
                    }
                    // remove extra dummy characters if any (should be exactly d, so nothing to remove)
                    // but since Insert already added d chars, no removal needed.

                    count++;
                    index = src.ToString().IndexOf(str1, index + len2, StringComparison.Ordinal);
                }
            }
            else if (d < 0) // shrink and replace mode
            {
                while (index != -1)
                {
                    // replace str1 by str2
                    for (int i = 0; i < len2; i++)
                    {
                        src[index + i] = str2[i];
                    }

                    // remove extra characters from the string (shrink)
                    src.Remove(index + len2, -d);

                    count++;
                    index = src.ToString().IndexOf(str1, index + len2, StringComparison.Ordinal);
                }
            }
            else // d == 0, just replace match
            {
                while (index != -1)
                {
                    for (int i = 0; i < len2; i++)
                    {
                        src[index + i] = str2[i];
                    }

                    count++;
                    index = src.ToString().IndexOf(str1, index + len2, StringComparison.Ordinal);
                }
            }

            return count;
        }

        /// <summary>
        /// Quake-style sprintf implementation
        /// </summary>
        /// <param name="buf">StringBuilder to receive formatted output</param>
        /// <param name="format">Format string</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Length of formatted string</returns>
        public static int BG_sprintf(StringBuilder buf, string format, params object[] args)
        {
            buf.Clear();
            return Q_vsprintf(buf, format, args);
        }
    }
}
