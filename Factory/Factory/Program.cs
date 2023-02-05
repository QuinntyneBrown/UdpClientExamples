// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

var udpClientFactory = new UdpClientFactory();

var udpClient1 = udpClientFactory.Create();

var udpClient2 = udpClientFactory.Create();

_ = Task.Run(async () =>
{
    var result = await udpClient1.ReceiveAsync();

    tcs.SetResult();
});

var bytesToSend = new byte[0];

await udpClient2.SendAsync(bytesToSend, UdpClientFactory.MultiCastGroupIp, UdpClientFactory.BroadcastPort);

await tcs.Task;
