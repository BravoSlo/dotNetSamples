Console.Title = "si.gapi.samples.periodicTimer";
Console.WriteLine("si.gapi.samples.periodicTimer");

CancellationTokenSource cts = new CancellationTokenSource();
CancellationToken ct = cts.Token;

PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));

Task worker = Task.Run(async () => {
	int iteration = 0;
	try {
		while(await timer.WaitForNextTickAsync(ct)) {
			string message = $"iteration {iteration++}";
			Console.WriteLine($"{message}");
		}
	} catch(OperationCanceledException oce) {
		Console.WriteLine($"Operation cancelled: {oce.Message}");
	} 
	catch(Exception ex) {
		Console.WriteLine(ex);	
	}
});

Task cancel = Task.Run(async () => {
	await Task.Delay(TimeSpan.FromSeconds(10));
	Console.WriteLine("canceling");
	cts.Cancel();
});

Console.WriteLine("running");
Console.ReadLine();