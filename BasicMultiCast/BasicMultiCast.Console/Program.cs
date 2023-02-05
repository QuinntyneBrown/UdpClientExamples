// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

string multiCastGroupIp = "224.0.0.1";

var broadCastPort = 80;

var udpClient1 = new UdpClient();

udpClient1.Client.Bind(IPEndPoint.Parse($"127.0.0.1:{broadCastPort}"));

udpClient1.JoinMulticastGroup(IPAddress.Parse(multiCastGroupIp), IPAddress.Parse("127.0.0.1"));

var udpClient2 = new UdpClient();

udpClient2.Client.Bind(IPEndPoint.Parse($"127.0.0.2:{broadCastPort}"));

udpClient2.JoinMulticastGroup(IPAddress.Parse(multiCastGroupIp), IPAddress.Parse("127.0.0.2"));

_ = Task.Run(async () =>
{
    var result = await udpClient1.ReceiveAsync();

    tcs.SetResult();
});

var bytesToSend = new byte[0];

await udpClient2.SendAsync(bytesToSend, multiCastGroupIp, broadCastPort);

await tcs.Task;
