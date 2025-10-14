using System.Threading.Channels;

Console.Title = "si.gapi.samples.channelsAsAsyncQueue";
Console.WriteLine("si.gapi.samples.channelsAsAsyncQueue");

CancellationTokenSource cts = new CancellationTokenSource();
CancellationToken ct = cts.Token;
Random rnd = new Random(DateTime.Now.Microsecond);

Channel<string> channel = Channel.CreateBounded<string>(
	new BoundedChannelOptions(5) {
		FullMode = BoundedChannelFullMode.Wait
	});

PeriodicTimer timerProducer = new PeriodicTimer(TimeSpan.FromMilliseconds(10));
Task producer = Task.Run(async () => {
	int iteration = 0;
	try {
		while(await timerProducer.WaitForNextTickAsync(ct)) {
			string message = $"iteration {iteration++}";
			await channel.Writer.WriteAsync(message);
			Console.WriteLine($"==> {message}");
		}
	} catch(OperationCanceledException oce) {
		Console.WriteLine($"Operation cancelled: {oce.Message}");
	} 
	catch(Exception ex) {
		Console.WriteLine(ex);	
	}
});

Task consumer = Task.Run(async () => {
	try {
		await foreach(var message in channel.Reader.ReadAllAsync(ct)) {
			Console.WriteLine($"<== {message}");
			await Task.Delay(rnd.Next(2, 30));
		}
	} catch(OperationCanceledException oce) {
		Console.WriteLine($"Operation cancelled: {oce.Message}");
	} catch(Exception ex) {
		Console.WriteLine(ex);
	}
});

Task cancel = Task.Run(async () => {
	await Task.Delay(TimeSpan.FromSeconds(10));
	cts.Cancel();
});

Console.WriteLine("running");
Console.ReadLine();