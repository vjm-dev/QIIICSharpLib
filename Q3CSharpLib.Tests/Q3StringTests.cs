using Xunit;
using Q3CSharpLib;
using System.Text;

namespace Q3CSharpLib.Tests
{
    public class Q3StringTests
    {
        [Fact]
        public void Q_vsprintf_ExpectedValue()
        {
            string input = "Inverse fast";
            float input2 = QMath.Q_rsqrt(42);
            string expected = "Inverse fast square root calculated: 0.15403557";

            var sb = new StringBuilder();
            QLib.Q_vsprintf(sb, "%s square root calculated: %.8f", new object[] { input, input2 });

            // QShared.Com_Printf(sb.ToString()); // "Inverse fast square root calculated: 0.15403557"
            string actual = sb.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
