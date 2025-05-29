# QIIICSharpLib
A C# port of key utility functions and data structures from the original Quake III Arena source code.
This NuGet package was built as a fun programming exercise, with a focus on learning, exploration, and nostalgia.

It reimagines core building blocks of the engine in a managed, modern language—while keeping the spirit of the original logic intact.

## Overview
Includes C# reimplementations of various low-level C routines:

- String utilities like `Q_stristr` and `BG_stricmp`

- Custom formatters such as `Q_vsprintf` and `BG_sprintf`

- Parsers and tokenizers mimicking Quake's internal data flow

- Math helpers, including the famous fast inverse square root hack

All functions have been adapted to C# idioms where needed, but aim to remain faithful to the original design.

## Purpose
Intended for:

- Learning: Understanding the inner workings of a classic game engine by examining its core components.

- Experimentation: Exploring how low-level C constructs can be represented and utilized in C#.

- Development: Providing a foundation for building tools or applications that interact with Quake III data formats or logic.

## Usage example
Before, install the package.

After... enjoy!
```cs
using Q3CSharpLib;
using System.Text;

var sb = new StringBuilder();
QLib.Q_vsprintf(sb, "%s square root calculated: %.8f", new object[] { "Inverse fast", QMath.Q_rsqrt(42) });

QShared.Com_Printf(sb.ToString()); // "Inverse fast square root calculated: 0.15403557"
```

## License
This is a coding hobby project based on open-source Quake III code.
You may use it under the [GPLv3 license (look here)](LICENSE)