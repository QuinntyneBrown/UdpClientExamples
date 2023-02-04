// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

var udpClient1 = new UdpClient();

udpClient1.Client.Bind(IPEndPoint.Parse("127.0.0.1:80"));

var udpClient2 = new UdpClient();

udpClient2.Client.Bind(IPEndPoint.Parse("127.0.0.2:80"));

_ = Task.Run(async () =>
{
    var result = await udpClient1.ReceiveAsync();

    tcs.SetResult();
});

var bytesToSend = new byte[0];

await udpClient2.SendAsync(bytesToSend, IPEndPoint.Parse("127.0.0.1:80"));

await tcs.Task;
