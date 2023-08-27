Directory.SetCurrentDirectory(AppContext.BaseDirectory);

Console.WriteLine("---------- SharpHook Sample ----------\n");

using var logSource = LogSource.Register(minLevel: LogLevel.Debug);
using var reactiveLogSource = new ReactiveLogSourceAdapter(logSource);

reactiveLogSource.MessageLogged.Subscribe(
    (logEntry) => Console.WriteLine($"{Enum.GetName(logEntry.Level)?.ToUpper()}: {logEntry.FullText}"));

var provider = UioHookProvider.Instance;

Console.WriteLine("System info:");
Console.WriteLine($"Auto-repeat rate: {provider.GetAutoRepeatRate()}");
Console.WriteLine($"Auto-repeat delay: {provider.GetAutoRepeatDelay()}");
Console.WriteLine($"Pointer acceleration multiplier: {provider.GetPointerAccelerationMultiplier()}");
Console.WriteLine($"Pointer acceleration threshold: {provider.GetPointerAccelerationThreshold()}");
Console.WriteLine($"Pointer sensitivity: {provider.GetPointerSensitivity()}");
Console.WriteLine($"Multi-click time: {provider.GetMultiClickTime()}");

var screens = provider.CreateScreenInfo();

foreach (var screen in screens)
{
    Console.WriteLine($"Screen #{screen.Number}: {screen.Width}x{screen.Height}; ({screen.X}, {screen.Y})");
}

Console.WriteLine();

logSource.MinLevel = LogLevel.Info;

Console.WriteLine("---------- Press q to quit ----------\n");

/********* Test hook
var hook = new SimpleReactiveGlobalHook(TaskPoolScheduler.Default);

hook.HookEnabled.Subscribe(OnHookEvent);
hook.HookDisabled.Subscribe(OnHookEvent);

hook.KeyTyped.Subscribe(OnHookEvent);
hook.KeyPressed.Subscribe(OnHookEvent);
hook.KeyReleased.Subscribe(OnHookEvent);
hook.KeyReleased.Subscribe(e => OnKeyReleased(e, hook));

hook.MouseClicked.Subscribe(OnHookEvent);
hook.MousePressed.Subscribe(OnHookEvent);
hook.MouseReleased.Subscribe(OnHookEvent);

hook.MouseMoved
    .Throttle(TimeSpan.FromSeconds(1))
    .Subscribe(OnHookEvent);

hook.MouseDragged
    .Throttle(TimeSpan.FromSeconds(1))
    .Subscribe(OnHookEvent);

hook.MouseWheel.Subscribe(OnHookEvent);

hook.Run();

static void OnHookEvent(HookEventArgs e) =>
    Console.WriteLine($"{e.EventTime.ToLocalTime()}: {e.RawEvent}");

static void OnKeyReleased(KeyboardHookEventArgs e, IReactiveGlobalHook hook)
{
    if (e.Data.KeyCode == KeyCode.VcQ)
    {
        hook.Dispose();
    }
}
*/

var simulator = new EventSimulator();
simulator.SimulateMouseMovement(3200, 450);
simulator.SimulateMousePress(3200, 450, MouseButton.Button1);
simulator.SimulateMouseRelease(3200, 450, MouseButton.Button1);

simulator.SimulateKeyPress(KeyCode.VcLeftControl);
simulator.SimulateKeyPress(KeyCode.VcA);
simulator.SimulateKeyRelease(KeyCode.VcA);
simulator.SimulateKeyRelease(KeyCode.VcLeftControl);

simulator.SimulateTextEntry("`1234567890-=qwertyuiop[]asdfghjkl;'\\zxcvbnm,./");
simulator.SimulateKeyPress(KeyCode.VcEnter);
simulator.SimulateKeyRelease(KeyCode.VcEnter);
simulator.SimulateKeyPress(KeyCode.VcEnter);
simulator.SimulateKeyRelease(KeyCode.VcEnter);
simulator.SimulateTextEntry("~!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL:\"|ZXCVBNM<>?");
simulator.SimulateKeyPress(KeyCode.VcEnter);
simulator.SimulateKeyRelease(KeyCode.VcEnter);
simulator.SimulateKeyPress(KeyCode.VcEnter);
simulator.SimulateKeyRelease(KeyCode.VcEnter);

// Move the mouse pointer 50 pixels to the right and 100 pixels down
simulator.SimulateMouseMovementRelative(50, 100);

// Scroll the mouse wheel
simulator.SimulateMouseWheel(
    rotation: -120,
    direction: MouseWheelScrollDirection.Vertical, // MouseWheelScrollDirection.Vertical by default
    type: MouseWheelScrollType.UnitScroll); // MouseWheelScrollType.UnitScroll by default
