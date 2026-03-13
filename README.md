Jeanette Nørgaard rbv 382

https://github.com/DIKU-SU/a6-jeanxnor

Build and run Person/Program.cs from the project directory using: 
dotnet run  
Build and run Library/Tests/Tests.cs from the directory using:  
dotnet test

--- GenAI citations and reflections ---

During this assignment I used Claude (Anthropic) as a learning aid to help understand and
implement object-oriented design patterns in C#.

Topics covered with AI assistance:

Understanding C# interfaces — what they are, what belongs in them, and what doesn't
Designing an ISquadron interface with a default method implementation
Separating concerns — keeping randomness and layout logic in the right places
Constructor injection — passing an origin point into a squadron rather than generating it
  internally
Using Vector2 for positions and understanding float literals (0.5f)
Understanding factory functions (Func<T>) and how they create per-enemy strategy instances
Refactoring hardcoded enemy instantiation into a reusable, interface-driven pattern
Moving enemy creation out of Game into a clean CreateSquadron method
Debugging compiler errors (CS0246, CS0103, missing interface members)
Understanding random range generation with NextSingle()
Understanding NullReferenceException — fields declared but never assigned because a method
  wasn't being called from the constructor
Warning CS8618 — non-nullable fields must be initialised, solved by providing default empty
  values at declaration
CS0102 duplicate field definitions — having both an uninitialised and initialised declaration
  of the same field
Factory lambdas () => new NoMovement() — how Func<T> works in practice as a per-enemy
  strategy factory
Random.NextSingle() for float random values vs Random.Next() for integers
Calling CreateSquadron() from the constructor to ensure fields are populated before Render runs
Creating concrete implementations (NoMovement, DefaultHit) of interfaces, separate from the
  interface files themselves

Using index % columns and index / columns to convert a flat index into row and column for a
  grid layout
Skipping a grid position (box without middle) using index adjustment —
  index >= n ? index + 1 : index
Accumulating row counts to find which row a flat index belongs to, for formations where rows
  have different lengths (diamond)
Centering uneven rows using startColumn = Math.Abs(row - 2) as an X offset
Zigzag offset — adding row * 0.05f to X to shift every second row without an if statement
GetSafeOrigin on the interface so each formation controls its own spawn bounds, making
  CreateSquadron formation-agnostic
Creating the squadron with a temporary Vector2.Zero origin first, calling GetSafeOrigin,
  then recreating with the real origin
Suggested using a switch expression for random formation selection in CreateSquadron

Suggested [TestCase] as a concise way to run the same test against multiple formations
Figuring out the structure and loop for iterating all enemy positions in the test
Asserting X and Y bounds with Is.InRange for every position index
Verifying numEnemies matches expected counts per formation with hardcoded expected values
  in a switch
Floating point precision — 0.5f cannot be represented exactly in binary, causing
  0.49999997f to fail an Is.InRange(0.5f, 1.0f) assertion. Fixed by using 0.49f as lower
  bound, with a comment explaining why
Identifying that the test origin needed to be fixed rather than random to ensure
  deterministic and repeatable tests

Drafting assistance:
Claude helped draft and iteratively refine this AI usage log throughout the assignment,
summarising topics after each major section was completed and incorporating feedback to
ensure the log accurately reflected the nature and extent of AI assistance received.

Nature of assistance:
The AI did not write the assignment for me — it answered questions, explained concepts,
corrected syntax errors, and helped me understand why design decisions are made, not just
what to write.




