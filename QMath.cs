using System.Numerics;

namespace Q3CSharpLib
{
    public static class QMath
    {
        public const int PITCH      = 0;
        public const int YAW        = 1;
        public const int ROLL       = 2;

        public const float M_PI     = 3.14159265358979323846f;

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="a">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float DEG2RAD(float a) => (a * M_PI) / 180.0f;

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="a">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static float RAD2DEG(float a) => (a * 180.0f) / M_PI;

        public static readonly Vector3 vec3_origin = new Vector3(0, 0, 0);

        public static readonly Vector3[] axisDefault =
        [
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        ];

        public const int nanmask = (255<<23);

        /// <summary>
        /// Checks if a float is NaN using bitmask comparison
        /// </summary>
        /// <param name="x">Float value to check</param>
        /// <returns>True if value is NaN</returns>
        public static bool IS_NAN(float x)
        {
            int bits = BitConverter.SingleToInt32Bits(x);
            return (bits & nanmask) == nanmask;
        }

        /// <summary>
        /// Fast square root approximation using reciprocal square root
        /// </summary>
        /// <param name="x">Input value</param>
        /// <returns>Approximate square root</returns>
        public static float SQRTFAST(float x) => x * Q_rsqrt(x);

        /// <summary>
        /// Converts an angle in degrees to a packed short integer representation
        /// </summary>
        /// <param name="x">Angle in degrees (0-360 range)</param>
        /// <returns>16-bit unsigned short representing the angle</returns>
        public static ushort ANGLE2SHORT(float x) => (ushort)((x * 65536f / 360f) % 65536);

        /// <summary>
        /// Converts a packed short integer back to an angle in degrees
        /// </summary>
        /// <param name="x">16-bit unsigned short from ANGLE2SHORT</param>
        /// <returns>Original angle in degrees (0-360 range)</returns>
        public static float SHORT2ANGLE(ushort x) => x * (360f / 65536f);

        /// <summary>
        /// Computes the square of a floating-point value
        /// </summary>
        /// <param name="x">Input value</param>
        /// <returns>x multiplied by itself (x²)</returns>
        public static float Square(float x) => x * x;

        public static readonly Vector4 colorBlack       = new Vector4(0, 0, 0, 1);
        public static readonly Vector4 colorRed         = new Vector4(1, 0, 0, 1);
        public static readonly Vector4 colorGreen       = new Vector4(0, 1, 0, 1);
        public static readonly Vector4 colorBlue        = new Vector4(0, 0, 1, 1);
        public static readonly Vector4 colorYellow      = new Vector4(1, 1, 0, 1);
        public static readonly Vector4 colorMagenta     = new Vector4(1, 0, 1, 1);
        public static readonly Vector4 colorCyan        = new Vector4(0, 1, 1, 1);
        public static readonly Vector4 colorWhite       = new Vector4(1, 1, 1, 1);
        public static readonly Vector4 colorLtGrey      = new Vector4(0.75f, 0.75f, 0.75f, 1);
        public static readonly Vector4 colorMdGrey      = new Vector4(0.5f, 0.5f, 0.5f, 1);
        public static readonly Vector4 colorDkGrey      = new Vector4(0.25f, 0.25f, 0.25f, 1);

        public static readonly Vector4[] g_color_table =
        [
            new Vector4(0f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(0f, 1f, 0f, 1f),
            new Vector4(1f, 1f, 0f, 1f),
            new Vector4(0f, 0f, 1f, 1f),
            new Vector4(0f, 1f, 1f, 1f),
            new Vector4(1f, 0f, 1f, 1f),
            new Vector4(1f, 1f, 1f, 1f)
        ];

        public const int NUMVERTEXNORMALS = 162;

        public static readonly Vector3[] bytedirs = new Vector3[NUMVERTEXNORMALS]
        {
            new Vector3(-0.525731f, 0.000000f, 0.850651f), new Vector3(-0.442863f, 0.238856f, 0.864188f),
            new Vector3(-0.295242f, 0.000000f, 0.955423f), new Vector3(-0.309017f, 0.500000f, 0.809017f),
            new Vector3(-0.162460f, 0.262866f, 0.951056f), new Vector3(0.000000f, 0.000000f, 1.000000f),
            new Vector3(0.000000f, 0.850651f, 0.525731f), new Vector3(-0.147621f, 0.716567f, 0.681718f),
            new Vector3(0.147621f, 0.716567f, 0.681718f), new Vector3(0.000000f, 0.525731f, 0.850651f),
            new Vector3(0.309017f, 0.500000f, 0.809017f), new Vector3(0.525731f, 0.000000f, 0.850651f),
            new Vector3(0.295242f, 0.000000f, 0.955423f), new Vector3(0.442863f, 0.238856f, 0.864188f),
            new Vector3(0.162460f, 0.262866f, 0.951056f), new Vector3(-0.681718f, 0.147621f, 0.716567f),
            new Vector3(-0.809017f, 0.309017f, 0.500000f), new Vector3(-0.587785f, 0.425325f, 0.688191f),
            new Vector3(-0.850651f, 0.525731f, 0.000000f), new Vector3(-0.864188f, 0.442863f, 0.238856f),
            new Vector3(-0.716567f, 0.681718f, 0.147621f), new Vector3(-0.688191f, 0.587785f, 0.425325f),
            new Vector3(-0.500000f, 0.809017f, 0.309017f), new Vector3(-0.238856f, 0.864188f, 0.442863f),
            new Vector3(-0.425325f, 0.688191f, 0.587785f), new Vector3(-0.716567f, 0.681718f, -0.147621f),
            new Vector3(-0.500000f, 0.809017f, -0.309017f), new Vector3(-0.525731f, 0.850651f, 0.000000f),
            new Vector3(0.000000f, 0.850651f, -0.525731f), new Vector3(-0.238856f, 0.864188f, -0.442863f),
            new Vector3(0.000000f, 0.955423f, -0.295242f), new Vector3(-0.262866f, 0.951056f, -0.162460f),
            new Vector3(0.000000f, 1.000000f, 0.000000f), new Vector3(0.000000f, 0.955423f, 0.295242f),
            new Vector3(-0.262866f, 0.951056f, 0.162460f), new Vector3(0.238856f, 0.864188f, 0.442863f),
            new Vector3(0.262866f, 0.951056f, 0.162460f), new Vector3(0.500000f, 0.809017f, 0.309017f),
            new Vector3(0.238856f, 0.864188f, -0.442863f), new Vector3(0.262866f, 0.951056f, -0.162460f),
            new Vector3(0.500000f, 0.809017f, -0.309017f), new Vector3(0.850651f, 0.525731f, 0.000000f),
            new Vector3(0.716567f, 0.681718f, 0.147621f), new Vector3(0.716567f, 0.681718f, -0.147621f),
            new Vector3(0.525731f, 0.850651f, 0.000000f), new Vector3(0.425325f, 0.688191f, 0.587785f),
            new Vector3(0.864188f, 0.442863f, 0.238856f), new Vector3(0.688191f, 0.587785f, 0.425325f),
            new Vector3(0.809017f, 0.309017f, 0.500000f), new Vector3(0.681718f, 0.147621f, 0.716567f),
            new Vector3(0.587785f, 0.425325f, 0.688191f), new Vector3(0.955423f, 0.295242f, 0.000000f),
            new Vector3(1.000000f, 0.000000f, 0.000000f), new Vector3(0.951056f, 0.162460f, 0.262866f),
            new Vector3(0.850651f, -0.525731f, 0.000000f), new Vector3(0.955423f, -0.295242f, 0.000000f),
            new Vector3(0.864188f, -0.442863f, 0.238856f), new Vector3(0.951056f, -0.162460f, 0.262866f),
            new Vector3(0.809017f, -0.309017f, 0.500000f), new Vector3(0.681718f, -0.147621f, 0.716567f),
            new Vector3(0.850651f, 0.000000f, 0.525731f), new Vector3(0.864188f, 0.442863f, -0.238856f),
            new Vector3(0.809017f, 0.309017f, -0.500000f), new Vector3(0.951056f, 0.162460f, -0.262866f),
            new Vector3(0.525731f, 0.000000f, -0.850651f), new Vector3(0.681718f, 0.147621f, -0.716567f),
            new Vector3(0.681718f, -0.147621f, -0.716567f), new Vector3(0.850651f, 0.000000f, -0.525731f),
            new Vector3(0.809017f, -0.309017f, -0.500000f), new Vector3(0.864188f, -0.442863f, -0.238856f),
            new Vector3(0.951056f, -0.162460f, -0.262866f), new Vector3(0.147621f, 0.716567f, -0.681718f),
            new Vector3(0.309017f, 0.500000f, -0.809017f), new Vector3(0.425325f, 0.688191f, -0.587785f),
            new Vector3(0.442863f, 0.238856f, -0.864188f), new Vector3(0.587785f, 0.425325f, -0.688191f),
            new Vector3(0.688191f, 0.587785f, -0.425325f), new Vector3(-0.147621f, 0.716567f, -0.681718f),
            new Vector3(-0.309017f, 0.500000f, -0.809017f), new Vector3(0.000000f, 0.525731f, -0.850651f),
            new Vector3(-0.525731f, 0.000000f, -0.850651f), new Vector3(-0.442863f, 0.238856f, -0.864188f),
            new Vector3(-0.295242f, 0.000000f, -0.955423f), new Vector3(-0.162460f, 0.262866f, -0.951056f),
            new Vector3(0.000000f, 0.000000f, -1.000000f), new Vector3(0.295242f, 0.000000f, -0.955423f),
            new Vector3(0.162460f, 0.262866f, -0.951056f), new Vector3(-0.442863f, -0.238856f, -0.864188f),
            new Vector3(-0.309017f, -0.500000f, -0.809017f), new Vector3(-0.162460f, -0.262866f, -0.951056f),
            new Vector3(0.000000f, -0.850651f, -0.525731f), new Vector3(-0.147621f, -0.716567f, -0.681718f),
            new Vector3(0.147621f, -0.716567f, -0.681718f), new Vector3(0.000000f, -0.525731f, -0.850651f),
            new Vector3(0.309017f, -0.500000f, -0.809017f), new Vector3(0.442863f, -0.238856f, -0.864188f),
            new Vector3(0.162460f, -0.262866f, -0.951056f), new Vector3(0.238856f, -0.864188f, -0.442863f),
            new Vector3(0.500000f, -0.809017f, -0.309017f), new Vector3(0.425325f, -0.688191f, -0.587785f),
            new Vector3(0.716567f, -0.681718f, -0.147621f), new Vector3(0.688191f, -0.587785f, -0.425325f),
            new Vector3(0.587785f, -0.425325f, -0.688191f), new Vector3(0.000000f, -0.955423f, -0.295242f),
            new Vector3(0.000000f, -1.000000f, 0.000000f), new Vector3(0.262866f, -0.951056f, -0.162460f),
            new Vector3(0.000000f, -0.850651f, 0.525731f), new Vector3(0.000000f, -0.955423f, 0.295242f),
            new Vector3(0.238856f, -0.864188f, 0.442863f), new Vector3(0.262866f, -0.951056f, 0.162460f),
            new Vector3(0.500000f, -0.809017f, 0.309017f), new Vector3(0.716567f, -0.681718f, 0.147621f),
            new Vector3(0.525731f, -0.850651f, 0.000000f), new Vector3(-0.238856f, -0.864188f, -0.442863f),
            new Vector3(-0.500000f, -0.809017f, -0.309017f), new Vector3(-0.262866f, -0.951056f, -0.162460f),
            new Vector3(-0.850651f, -0.525731f, 0.000000f), new Vector3(-0.716567f, -0.681718f, -0.147621f),
            new Vector3(-0.716567f, -0.681718f, 0.147621f), new Vector3(-0.525731f, -0.850651f, 0.000000f),
            new Vector3(-0.500000f, -0.809017f, 0.309017f), new Vector3(-0.238856f, -0.864188f, 0.442863f),
            new Vector3(-0.262866f, -0.951056f, 0.162460f), new Vector3(-0.864188f, -0.442863f, 0.238856f),
            new Vector3(-0.809017f, -0.309017f, 0.500000f), new Vector3(-0.688191f, -0.587785f, 0.425325f),
            new Vector3(-0.681718f, -0.147621f, 0.716567f), new Vector3(-0.442863f, -0.238856f, 0.864188f),
            new Vector3(-0.587785f, -0.425325f, 0.688191f), new Vector3(-0.309017f, -0.500000f, 0.809017f),
            new Vector3(-0.147621f, -0.716567f, 0.681718f), new Vector3(-0.425325f, -0.688191f, 0.587785f),
            new Vector3(-0.162460f, -0.262866f, 0.951056f), new Vector3(0.442863f, -0.238856f, 0.864188f),
            new Vector3(0.162460f, -0.262866f, 0.951056f), new Vector3(0.309017f, -0.500000f, 0.809017f),
            new Vector3(0.147621f, -0.716567f, 0.681718f), new Vector3(0.000000f, -0.525731f, 0.850651f),
            new Vector3(0.425325f, -0.688191f, 0.587785f), new Vector3(0.587785f, -0.425325f, 0.688191f),
            new Vector3(0.688191f, -0.587785f, 0.425325f), new Vector3(-0.955423f, 0.295242f, 0.000000f),
            new Vector3(-0.951056f, 0.162460f, 0.262866f), new Vector3(-1.000000f, 0.000000f, 0.000000f),
            new Vector3(-0.850651f, 0.000000f, 0.525731f), new Vector3(-0.955423f, -0.295242f, 0.000000f),
            new Vector3(-0.951056f, -0.162460f, 0.262866f), new Vector3(-0.864188f, 0.442863f, -0.238856f),
            new Vector3(-0.951056f, 0.162460f, -0.262866f), new Vector3(-0.809017f, 0.309017f, -0.500000f),
            new Vector3(-0.864188f, -0.442863f, -0.238856f), new Vector3(-0.951056f, -0.162460f, -0.262866f),
            new Vector3(-0.809017f, -0.309017f, -0.500000f), new Vector3(-0.681718f, 0.147621f, -0.716567f),
            new Vector3(-0.681718f, -0.147621f, -0.716567f), new Vector3(-0.850651f, 0.000000f, -0.525731f),
            new Vector3(-0.688191f, 0.587785f, -0.425325f), new Vector3(-0.587785f, 0.425325f, -0.688191f),
            new Vector3(-0.425325f, 0.688191f, -0.587785f), new Vector3(-0.425325f, -0.688191f, -0.587785f),
            new Vector3(-0.587785f, -0.425325f, -0.688191f), new Vector3(-0.688191f, -0.587785f, -0.425325f)
        };

        //=============================================================================================================

        /// <summary>
        /// Sets a vector to (0,0,0)
        /// </summary>
        /// <param name="v">Output vector</param>
        public static void VectorClear(out Vector3 v) => v = Vector3.Zero;

        /// <summary>
        /// Computes dot product of two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Dot product result</returns>
        public static float DotProduct(Vector3 v1, Vector3 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        /// <summary>
        /// Subtracts two vectors (c = a - b)
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <param name="c">Result vector</param>
        public static void VectorSubtract(Vector3 a, Vector3 b, out Vector3 c) => c = a - b;

        /// <summary>
        /// Adds two vectors (c = a + b)
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <param name="c">Result vector</param>
        public static void VectorAdd(Vector3 a, Vector3 b, out Vector3 c) => c = a + b;

        /// <summary>
        /// Copies vector components
        /// </summary>
        /// <param name="v1">Source vector</param>
        /// <param name="v2">Destination vector</param>
        public static void VectorCopy(Vector3 v1, out Vector3 v2) => v2 = v1;

        /// <summary>
        /// Scales vector components (o = v * s)
        /// </summary>
        /// <param name="v">Base vector</param>
        /// <param name="s">Scale vector</param>
        /// <param name="o">Result vector</param>
        public static void VectorScale(Vector3 v, Vector3 s, out Vector3 o) => o = v * s;

        /// <summary>
        /// Vector multiply-add operation (o = v + b * scale)
        /// </summary>
        /// <param name="v">Base vector</param>
        /// <param name="s">Scale factor</param>
        /// <param name="b">Direction vector</param>
        /// <param name="o">Result vector</param>
        public static void VectorMA(Vector3 v, float s, Vector3 b, out Vector3 o) => o = v + b * s;

        /// <summary>
        /// Scales a Vector4 by scalar value
        /// </summary>
        /// <param name="input">Input vector</param>
        /// <param name="scale">Scale factor</param>
        /// <param name="output">Result vector</param>
        public static void Vector4Scale(Vector4 input, float scale, out Vector4 output)
        {
	        output.W = input.W * scale;
	        output.X = input.X * scale;
	        output.Y = input.Y * scale;
	        output.Z = input.Z * scale;
        }

        //==============================================================

        /// <summary>
        /// Linear congruential random number generator
        /// </summary>
        /// <param name="seed">Seed value (modified in place)</param>
        /// <returns>New random integer</returns>
        public static int Q_rand(ref int seed)
        {
            var random = new Random(seed);
            return random.Next();
        }

        /// <summary>
        /// Generates random float in [0,1) range
        /// </summary>
        /// <param name="seed">Seed value (modified in place)</param>
        /// <returns>Random float</returns>
        public static float Q_random(ref int seed)
        {
            var random = new Random(seed);
            return (float)random.NextDouble();
        }

        /// <summary>
        /// Generates random float in [-1,1) range
        /// </summary>
        /// <param name="seed">Seed value (modified in place)</param>
        /// <returns>Random float</returns>
        public static float Q_crandom(ref int seed)
        {
            return 2.0f * (Q_random(ref seed) - 0.5f);
        }

        /// <summary>
        /// Compares two vectors for exact equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>True if vectors are identical</returns>
        public static bool VectorCompare(Vector3 v1, Vector3 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }

        /// <summary>
        /// Computes vector magnitude
        /// </summary>
        /// <param name="v">Input vector</param>
        /// <returns>Length of vector</returns>
        public static float VectorLength(Vector3 v)
        {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        /// <summary>
        /// Computes vector magnitude squared
        /// </summary>
        /// <param name="v">Input vector</param>
        /// <returns>Squared length of vector</returns>
        public static float VectorLengthSquared(Vector3 v)
        {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        /// <summary>
        /// Computes Euclidean distance between two points
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance between points</returns>
        public static float Distance(Vector3 p1, Vector3 p2)
        {
            VectorSubtract(p2, p1, out Vector3 v);
            return VectorLength(v);
        }

        /// <summary>
        /// Computes squared distance between two points
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Squared distance between points</returns>
        public static float DistanceSquared(Vector3 p1, Vector3 p2)
        {
            VectorSubtract(p2, p1, out Vector3 v);
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        /// <summary>
        /// Fast vector normalization using reciprocal square root approximation
        /// </summary>
        /// <param name="v">Vector to normalize (modified in place)</param>
        // fast vector normalize routine that does not check to make sure
        // that length != 0, nor does it return length, uses rsqrt approximation
        public static void VectorNormalizeFast(ref Vector3 v)
        {
            float ilength = Q_rsqrt(DotProduct(v, v));
            v.X *= ilength;
            v.Y *= ilength;
            v.Z *= ilength;
        }

        /// <summary>
        /// Negates a vector (v = -v)
        /// </summary>
        /// <param name="v">Vector to negate (modified in place)</param>
        public static void VectorInverse(ref Vector3 v)
        {
            v = -v;
        }

        /// <summary>
        /// Computes cross product of two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <param name="cross">Result vector</param>
        public static void CrossProduct(Vector3 v1, Vector3 v2, out Vector3 cross)
        {
            cross.X = v1.Y * v2.Z - v1.Z * v2.Y;
            cross.Y = v1.Z * v2.X - v1.X * v2.Z;
            cross.Z = v1.X * v2.Y - v1.Y * v2.X;
        }

        //=======================================================

        /// <summary>
        /// Clamps integer to sbyte range
        /// </summary>
        /// <param name="i">Input value</param>
        /// <returns>Clamped value</returns>
        public static sbyte ClampChar(int i)
        {
            return (sbyte)Math.Clamp(i, -128, 127);
        }

        /// <summary>
        /// Clamps integer to short range
        /// </summary>
        /// <param name="i">Input value</param>
        /// <returns>Clamped value</returns>
        public static short ClampShort(int i)
        {
            return (short)Math.Clamp(i, -32768, 32767);
        }

        /// <summary>
        /// Maps direction vector to byte index in normal table
        /// </summary>
        /// <param name="dir">Normalized direction vector</param>
        /// <returns>Index in bytedirs table (0 - 161)</returns>
        // this isn't a real cheap function to call!
        public static int DirToByte(Vector3 dir)
        {
            if (dir == Vector3.Zero) return 0;

            int best = 0;
            float bestd = 0;
            for (int i = 0; i < NUMVERTEXNORMALS; i++)
            {
                float d = Vector3.Dot(dir, bytedirs[i]);
                if (d > bestd)
                {
                    bestd = d;
                    best = i;
                }
            }
            return best;
        }

        /// <summary>
        /// Retrieves direction vector from byte index
        /// </summary>
        /// <param name="b">Index in normal table</param>
        /// <param name="dir">Result direction vector</param>
        public static void ByteToDir(int b, out Vector3 dir)
        {
            if (b < 0 || b >= NUMVERTEXNORMALS)
            {
                VectorClear(out dir);
                return;
            }
            dir = bytedirs[b];
        }

        /// <summary>
        /// Packs RGB values into uint (0x00RRGGBB)
        /// </summary>
        /// <param name="r">Red component (0.0f - 1.0f)</param>
        /// <param name="g">Green component (0.0f - 1.0f)</param>
        /// <param name="b">Blue component (0.0f - 1.0f)</param>
        /// <returns>Packed color value</returns>
        public static uint ColorBytes3(float r, float g, float b)
        {
            return (uint)((byte)(r * 255) | ((byte)(g * 255) << 8) | ((byte)(b * 255) << 16));
        }

        /// <summary>
        /// Packs RGBA values into uint (0xAARRGGBB)
        /// </summary>
        /// <param name="r">Red component (0.0f - 1.0f)</param>
        /// <param name="g">Green component (0.0f - 1.0f)</param>
        /// <param name="b">Blue component (0.0f - 1.0f)</param>
        /// <param name="a">Alpha component (0.0f - 1.0f)</param>
        /// <returns>Packed color value</returns>
        public static uint ColorBytes4(float r, float g, float b, float a)
        {
            return (uint)((byte)(r * 255) | ((byte)(g * 255) << 8) | ((byte)(b * 255) << 16) | ((byte)(a * 255) << 24));
        }

        /// <summary>
        /// Normalizes RGB color while preserving hue
        /// </summary>
        /// <param name="input">Input color vector</param>
        /// <param name="output">Normalized color vector</param>
        /// <returns>Original maximum channel value</returns>
        public static float NormalizeColor(Vector3 input, out Vector3 output)
        {
            float max = Math.Max(input.X, Math.Max(input.Y, input.Z));
            if (max == 0)
            {
                VectorClear(out output);
            }
            else
            {
                output = input / max;
            }
            return max;
        }

        /// <summary>
        /// Rotates point around axis by specified degrees
        /// </summary>
        /// <param name="dst">Result point</param>
        /// <param name="dir">Axis of rotation</param>
        /// <param name="point">Point to rotate</param>
        /// <param name="degrees">Rotation angle in degrees</param>
        public static void RotatePointAroundVector(out Vector3 dst, Vector3 dir, Vector3 point, float degrees)
        {
            float[][] m = new float[3][];
            float[][] im = new float[3][];
            float[][] zrot = new float[3][];
            float[][] tmpmat = new float[3][];
            float[][] rot = new float[3][];

            Vector3 vup, vr, vf = dir;
            PerpendicularVector(out vr, dir);
            CrossProduct(vr, vf, out vup);

            m[0][0] = vr.X; m[0][1] = vup.X; m[0][2] = vf.X;
            m[1][0] = vr.Y; m[1][1] = vup.Y; m[1][2] = vf.Y;
            m[2][0] = vr.Z; m[2][1] = vup.Z; m[2][2] = vf.Z;

            im[0][0] = m[0][0]; im[0][1] = m[1][0]; im[0][2] = m[2][0];
            im[1][0] = m[0][1]; im[1][1] = m[1][1]; im[1][2] = m[2][1];
            im[2][0] = m[0][2]; im[2][1] = m[1][2]; im[2][2] = m[2][2];

            float rad = DEG2RAD(degrees);

            zrot[0][0] = MathF.Cos(rad); zrot[0][1] = MathF.Sin(rad); zrot[0][2] = 0;
            zrot[1][0] = -MathF.Sin(rad); zrot[1][1] = MathF.Cos(rad); zrot[1][2] = 0;
            zrot[2][0] = 0; zrot[2][1] = 0; zrot[2][2] = 1;

            MatrixMultiply(m, zrot, ref tmpmat);
            MatrixMultiply(tmpmat, im, ref rot);

            dst = new Vector3(
                rot[0][0] * point.X + rot[0][1] * point.Y + rot[0][2] * point.Z,
                rot[1][0] * point.X + rot[1][1] * point.Y + rot[1][2] * point.Z,
                rot[2][0] * point.X + rot[2][1] * point.Y + rot[2][2] * point.Z
            );
        }

        /// <summary>
        /// Rotates orientation around forward axis
        /// </summary>
        /// <param name="axis">Basis vectors (modified in place)</param>
        /// <param name="yaw">Yaw angle in degrees</param>
        public static void RotateAroundDirection(Vector3[] axis, float yaw)
        {
            // create an arbitrary axis[1] 
            PerpendicularVector(out axis[1], axis[0]);

            // rotate axis[1] around axis[0] by yaw
            if (yaw != 0f)
            {
                Vector3 temp;
                VectorCopy(axis[1], out temp);
                RotatePointAroundVector(out axis[1], axis[0], temp, yaw);
            }

            // calculate the third axis as the cross product
            CrossProduct(axis[0], axis[1], out axis[2]);
        }

        /// <summary>
        /// Converts direction vector to Euler angles
        /// </summary>
        /// <param name="vec">Direction vector</param>
        /// <param name="angles">Result angles (pitch, yaw, roll)</param>
        public static void VecToAngles(Vector3 vec, out Vector3 angles)
        {
            float yaw, pitch;
            if (vec.X == 0 && vec.Y == 0)
            {
                yaw = 0;
                pitch = vec.Z > 0 ? 90 : 270;
            }
            else
            {
                yaw = MathF.Atan2(vec.Y, vec.X) * (180f / MathF.PI);
                if (yaw < 0) yaw += 360;

                float forward = MathF.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
                pitch = MathF.Atan2(vec.Z, forward) * (180f / MathF.PI);
                if (pitch < 0) pitch += 360;
            }

            angles = new Vector3(-pitch, yaw, 0);
        }

        /// <summary>
        /// Converts Euler angles to basis vectors
        /// </summary>
        /// <param name="angles">Input angles</param>
        /// <param name="axis">Output basis vectors (forward, right, up)</param>
        public static void AnglesToAxis(Vector3 angles, Vector3[] axis)
        {
            Vector3 right;

            // angle vectors returns "right" instead of "y axis"
            AngleVectors(angles, out axis[0], out right, out axis[2]);
            VectorSubtract(vec3_origin, right, out axis[1]);
        }

        /// <summary>
        /// Resets axis to identity matrix
        /// </summary>
        /// <param name="axis">Axis to reset (modified in place)</param>
        public static void AxisClear(ref Vector3[] axis)
        {
            axis[0].X = 1;
            axis[0].Y = 0;
            axis[0].Z = 0;
            axis[1].X = 0;
            axis[1].Y = 1;
            axis[1].Z = 0;
            axis[2].X = 0;
            axis[2].Y = 0;
            axis[2].Z = 1;
        }

        /// <summary>
        /// Copies axis matrix
        /// </summary>
        /// <param name="input">Source axis</param>
        /// <param name="output">Destination axis</param>
        public static void AxisCopy(Vector3[] input, ref Vector3[] output)
        {
            VectorCopy(input[0], out output[0]);
            VectorCopy(input[1], out output[1]);
            VectorCopy(input[2], out output[2]);
        }

        /// <summary>
        /// Projects point onto plane defined by normal
        /// </summary>
        /// <param name="dst">Projected point</param>
        /// <param name="p">Source point</param>
        /// <param name="normal">Plane normal</param>
        public static void ProjectPointOnPlane(out Vector3 dst, Vector3 p, Vector3 normal)
        {
            float invDenom = DotProduct(normal, normal);

            if (Q_fabs(invDenom) == 0.0f)
            {
                VectorCopy(p, out dst); // the normal vector is null, we return the point unchanged
                return;
            }

            invDenom = 1.0f / invDenom;

            float d = DotProduct(normal, p) * invDenom;

            Vector3 n = normal * invDenom;

            dst = p - d * n;
        }


        /// <summary>
        /// Creates orthogonal basis from forward vector
        /// </summary>
        /// <param name="forward">Input forward vector</param>
        /// <param name="right">Output right vector</param>
        /// <param name="up">Output up vector</param>
        public static void MakeNormalVectors(Vector3 forward, out Vector3 right, out Vector3 up)
        {
            float d;

            // this rotate and negate guarantees a vector
            // not colinear with the original
            right.Y = -forward.X;
	        right.Z = forward.Y;
	        right.X = forward.Z;

	        d = DotProduct(right, forward);
            VectorMA(right, -d, forward, out right);
            VectorNormalize(right);
            CrossProduct(right, forward, out up);
        }

        /// <summary>
        /// Rotates vector using axis matrix
        /// </summary>
        /// <param name="input">Input vector</param>
        /// <param name="matrix">Basis matrix</param>
        /// <param name="output">Result vector</param>
        public static void VectorRotate(Vector3 input, Vector3[] matrix, out Vector3 output )
        {
	        output.X = DotProduct(input, matrix[0]);
            output.Y = DotProduct(input, matrix[1]);
            output.Z = DotProduct(input, matrix[2]);
        }

        //============================================================================

        /// <summary>
        /// Fast reciprocal square root approximation
        /// </summary>
        /// <param name="number">Input value (>0)</param>
        /// <returns>Approximate 1/sqrt(number)</returns>
        public static float Q_rsqrt(float number)
        {
            const float threehalfs = 1.5f;

            float x2 = number * 0.5f;
            float y = number;
            int i = BitConverter.SingleToInt32Bits(y);  // in C used an evil floating point bit level hacking
            i = 0x5f3759df - (i >> 1);                  // what the fuck?
            y = BitConverter.Int32BitsToSingle(i);
            y = y * (threehalfs - x2 * y * y);          // 1st iteration
        //	y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed

            return y;
        }

        /// <summary>
        /// Absolute value of float
        /// </summary>
        /// <param name="f">Input value</param>
        /// <returns>Absolute value</returns>
        public static float Q_fabs(float f) => MathF.Abs(f);

        //============================================================================

        /// <summary>
        /// Linearly interpolates between two angles
        /// </summary>
        /// <param name="from">Start angle (degrees)</param>
        /// <param name="to">End angle (degrees)</param>
        /// <param name="frac">Interpolation factor (0.0f - 1.0f)</param>
        /// <returns>Interpolated angle</returns>
        public static float LerpAngle(float from, float to, float frac)
        {
            float a;

            if (to - from > 180)
            {
                to -= 360;
            }
            if (to - from < -180)
            {
                to += 360;
            }
            a = from + frac * (to - from);

            return a;
        }


        /// <summary>
        /// Computes angular difference normalized to [-180,180]
        /// </summary>
        /// <param name="a1">First angle (degrees)</param>
        /// <param name="a2">Second angle (degrees)</param>
        /// <returns>Normalized angular difference</returns>
        public static float AngleSubtract(float a1, float a2)
        {
            float a;

            a = a1 - a2;
            while (a > 180)
            {
                a -= 360;
            }
            while (a < -180)
            {
                a += 360;
            }
            return a;
        }

        /// <summary>
        /// Subtracts two angle vectors component-wise with angle normalization
        /// </summary>
        /// <param name="v1">Minuend angle vector (input)</param>
        /// <param name="v2">Subtrahend angle vector (input)</param>
        /// <param name="v3">Resulting angle difference vector (output)</param>
        public static void AnglesSubtract(Vector3 v1, Vector3 v2, out Vector3 v3)
        {
            v3.X = AngleSubtract(v1.X, v2.X);
            v3.Y = AngleSubtract(v1.Y, v2.Y);
            v3.Z = AngleSubtract(v1.Z, v2.Z);
        }

        /// <summary>
        /// Normalizes an angle to the range [0, 360) using integer modulus
        /// </summary>
        /// <param name="a">Input angle in degrees</param>
        /// <returns>Normalized angle in [0, 360)</returns>
        public static float AngleMod(float a)
        {
            a = (float)((360.0 / 65536) * ((int)(a * (65536 / 360.0)) & 65535));
            return a;
        }

        /// <summary>
        /// Normalizes an angle to the range [0, 360)
        /// </summary>
        /// <param name="angle">Input angle in degrees</param>
        /// <returns>Normalized angle in [0, 360)</returns>
        public static float AngleNormalize360(float angle)
        {
            return (float)(360.0 / 65536) * ((int)(angle * (65536 / 360.0)) & 65535);
        }

        /// <summary>
        /// Normalizes an angle to the range [-180, 180]
        /// </summary>
        /// <param name="angle">Input angle in degrees</param>
        /// <returns>Normalized angle in [-180, 180]</returns>
        public static float AngleNormalize180(float angle)
        {
            angle = AngleNormalize360(angle);
            if (angle > 180.0)
            {
                angle -= (float)360.0;
            }
            return angle;
        }

        /// <summary>
        /// Computes the smallest difference between two angles
        /// </summary>
        /// <param name="angle1">First angle in degrees</param>
        /// <param name="angle2">Second angle in degrees</param>
        /// <returns>Normalized delta angle difference in [-180, 180]</returns>
        public static float AngleDelta(float angle1, float angle2)
        {
            return AngleNormalize180(angle1 - angle2);
        }

        //============================================================================

        /// <summary>
        /// Computes bounding sphere radius for AABB
        /// </summary>
        /// <param name="mins">Minimum bounds</param>
        /// <param name="maxs">Maximum bounds</param>
        /// <returns>Radius of enclosing sphere</returns>
        public static float RadiusFromBounds(Vector3 mins, Vector3 maxs)
        {
            Vector3 corner = new Vector3(
                MathF.Max(MathF.Abs(mins.X), MathF.Abs(maxs.X)),
                MathF.Max(MathF.Abs(mins.Y), MathF.Abs(maxs.Y)),
                MathF.Max(MathF.Abs(mins.Z), MathF.Abs(maxs.Z))
            );

            return VectorLength(corner);
        }

        /// <summary>
        /// Initializes AABB to inverse limits
        /// </summary>
        /// <param name="mins">Minimum bounds (output)</param>
        /// <param name="maxs">Maximum bounds (output)</param>
        public static void ClearBounds(out Vector3 mins, out Vector3 maxs)
        {
            mins = new Vector3(99999f, 99999f, 99999f);
            maxs = new Vector3(-99999f, -99999f, -99999f);
        }

        /// <summary>
        /// Expands AABB to include point
        /// </summary>
        /// <param name="point">Point to include</param>
        /// <param name="mins">Minimum bounds (modified in place)</param>
        /// <param name="maxs">Maximum bounds (modified in place)</param>
        public static void AddPointToBounds(Vector3 point, ref Vector3 mins, ref Vector3 maxs)
        {
            if (point.X < mins.X) mins.X = point.X;
            if (point.X > maxs.X) maxs.X = point.X;

            if (point.Y < mins.Y) mins.Y = point.Y;
            if (point.Y > maxs.Y) maxs.Y = point.Y;

            if (point.Z < mins.Z) mins.Z = point.Z;
            if (point.Z > maxs.Z) maxs.Z = point.Z;
        }

        /// <summary>
        /// Normalizes vector and returns original length
        /// </summary>
        /// <param name="v">Vector to normalize (modified in place)</param>
        /// <returns>Original vector length</returns>
        public static float VectorNormalize(Vector3 v)
        {
            float length, ilength;

            length = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
            length = MathF.Sqrt(length);

            if (length != 0f)
            {
                ilength = 1 / length;
                v.X *= ilength;
                v.Y *= ilength;
                v.Z *= ilength;
            }

            return length;
        }

        /// <summary>
        /// Normalizes a vector and outputs the result to a separate vector
        /// </summary>
        /// <param name="v">Vector to normalize</param>
        /// <param name="outV">Normalized output vector</param>
        /// <returns>Original length of the vector</returns>
        public static float VectorNormalize2(Vector3 v, Vector3 outV)
        {
            float length, ilength;

            length = v.X* v.X + v.Y* v.Y + v.Z* v.Z;
            length = MathF.Sqrt(length);

	        if (length != 0f)
	        {
		        ilength = 1 / length;
		        outV.X = v.X * ilength;
		        outV.Y = v.Y * ilength;
		        outV.Z = v.Z * ilength;
            }
            else
            {
		        VectorClear(out outV);
            }

            return length;
        }

        /// <summary>
        /// Computes integer log₂ (floor)
        /// </summary>
        /// <param name="val">Input value (>0)</param>
        /// <returns>Floor of log₂(val)</returns>
        public static int Q_log2(int val)
        {
            int answer;

            answer = 0;
            while ((val >>= 1) != 0)
            {
                answer++;
            }
            return answer;
        }

        /// <summary>
        /// Multiplies two 3x3 matrices
        /// </summary>
        /// <param name="in1">First matrix</param>
        /// <param name="in2">Second matrix</param>
        /// <param name="output">Result matrix</param>
        public static void MatrixMultiply(float[][] in1, float[][] in2, ref float[][] output)
        {
            output[0][0] = in1[0][0] * in2[0][0] + in1[0][1] * in2[1][0] +
                        in1[0][2] * in2[2][0];
            output[0][1] = in1[0][0] * in2[0][1] + in1[0][1] * in2[1][1] +
                        in1[0][2] * in2[2][1];
            output[0][2] = in1[0][0] * in2[0][2] + in1[0][1] * in2[1][2] +
                        in1[0][2] * in2[2][2];
            output[1][0] = in1[1][0] * in2[0][0] + in1[1][1] * in2[1][0] +
                        in1[1][2] * in2[2][0];
            output[1][1] = in1[1][0] * in2[0][1] + in1[1][1] * in2[1][1] +
                        in1[1][2] * in2[2][1];
            output[1][2] = in1[1][0] * in2[0][2] + in1[1][1] * in2[1][2] +
                        in1[1][2] * in2[2][2];
            output[2][0] = in1[2][0] * in2[0][0] + in1[2][1] * in2[1][0] +
                        in1[2][2] * in2[2][0];
            output[2][1] = in1[2][0] * in2[0][1] + in1[2][1] * in2[1][1] +
                        in1[2][2] * in2[2][1];
            output[2][2] = in1[2][0] * in2[0][2] + in1[2][1] * in2[1][2] +
                        in1[2][2] * in2[2][2];
        }

        /// <summary>
        /// Converts Euler angles to direction vectors
        /// </summary>
        /// <param name="angles">Input angles (pitch, yaw, roll)</param>
        /// <param name="forward">Forward vector</param>
        /// <param name="right">Right vector</param>
        /// <param name="up">Up vector</param>
        public static void AngleVectors(Vector3 angles, out Vector3 forward, out Vector3 right, out Vector3 up)
        {
            float angle;
            float sr, sp, sy, cr, cp, cy;

            angle = angles.Y * (MathF.PI * 2f / 360f);
            sy = MathF.Sin(angle);
            cy = MathF.Cos(angle);

            angle = angles.X * (MathF.PI * 2f / 360f);
            sp = MathF.Sin(angle);
            cp = MathF.Cos(angle);

            angle = angles.Z * (MathF.PI * 2f / 360f);
            sr = MathF.Sin(angle);
            cr = MathF.Cos(angle);

            forward = new Vector3(
                cp * cy,
                cp * sy,
                -sp
            );

            right = new Vector3(
                -1 * sr * sp * cy + -1 * cr * -sy,
                -1 * sr * sp * sy + -1 * cr * cy,
                -1 * sr * cp
            );

            up = new Vector3(
                cr * sp * cy + -sr * -sy,
                cr * sp * sy + -sr * cy,
                cr * cp
            );
        }

        /// <summary>
        /// Finds perpendicular vector to input
        /// </summary>
        /// <param name="dst">Result vector</param>
        /// <param name="src">Input vector</param>
        public static void PerpendicularVector(out Vector3 dst, Vector3 src)
        {
            int pos = 0;
            float minelem = 1.0f;

            // find the smallest magnitude axially aligned vector
            for (int i = 0; i < 3; i++)
            {
                float val = MathF.Abs(src[i]);
                if (val < minelem)
                {
                    pos = i;
                    minelem = val;
                }
            }

            Vector3 tempvec = Vector3.Zero;
            tempvec[pos] = 1.0f;

            // project the point onto the plane defined by src
            ProjectPointOnPlane(out dst, tempvec, src);

            // normalize the result
            VectorNormalize(dst);
        }
    }
}
