// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Factory;

public class UdpClientFactory : IUdpClientFactory
{
    public static readonly string MultiCastGroupIp = "224.0.0.1";
    public UdpClient Create()
    {
        UdpClient udpClient = default;

        int i = 1;


        while (udpClient?.Client?.IsBound == null || udpClient.Client.IsBound == false)
        {
            try
            {
                udpClient = new UdpClient();

                udpClient.Client.Bind(IPEndPoint.Parse($"127.0.0.{i}:80"));

                udpClient.JoinMulticastGroup(IPAddress.Parse(MultiCastGroupIp), IPAddress.Parse($"127.0.0.{i}"));
            }
            catch(SocketException)
            {
                i++;
            }
        }

        return udpClient;
    }
}


