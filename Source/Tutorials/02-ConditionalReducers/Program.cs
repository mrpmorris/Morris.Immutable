using ConditionalReducers;
using Morris.Immutable;
using System.Text.Json;

// Create a conditional reducer.
// The result should be TState newState.
// The conditional reducer will change this to (bool Changed, TState newState)
// based on whether or not the condition triggered the reducer code.
var counterIncrementCounterReducer = Reducer
	.Given<CounterState, IncrementCounterAction>()
	.When((s, a) => s.Counter < 2)
	.Then((s, a) => s with { Counter = s.Counter + a.Delta });

var state = new CounterState(0);
var action = new IncrementCounterAction(1);

Console.WriteLine($"Original state={JsonSerializer.Serialize(state)}");
for (int i = 0; i < 3; i++)
{
	(bool changed, state) = counterIncrementCounterReducer(state, action);
	Console.ForegroundColor = changed ? ConsoleColor.Red : ConsoleColor.Green;
	Console.WriteLine($"Step={i + 1}, Changed={changed}, State={JsonSerializer.Serialize(state)}");
}
//	Output:
//		Original state={"Counter":0}
//		Step=1, Changed=True, State={"Counter":1}
//		Step=2, Changed=True, State={"Counter":2}
//		Step=3, Changed=False, State={"Counter":2}

Console.ForegroundColor = ConsoleColor.White;
Console.ReadLine();
