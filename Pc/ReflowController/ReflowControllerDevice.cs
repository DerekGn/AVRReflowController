using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.DeviceNotify;
using System;
using System.IO;
using Google.Protobuf;

namespace ReflowController
{
    internal class ReflowControllerDevice : IReflowControllerDevice, IDisposable
    {
        private const int ProductId = 0x206C;
        private const int VendorId = 0x03EB;

        private UsbEndpointReader _usbEndpointReader;
        private UsbEndpointWriter _usbEndpointWriter;
        private IDeviceNotifier _usbDeviceNotifier;
        private UsbDeviceFinder _usbFinder;
        private UsbDevice _usbDevice;

        public event EventHandler DeviceDisconnected;
        public event EventHandler DeviceConnected;
        public bool IsDeviceConnected
        {
            get
            {
                return false;
            }
        }
        public ReflowControllerDevice()
        {
            _usbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
            _usbDeviceNotifier.OnDeviceNotify += UsbDeviceNotifier_OnDeviceNotify;

            _usbFinder = new UsbDeviceFinder(VendorId, ProductId);
        }

        public ThermoCoupleStatus GetThermoCoupleStatus()
        {
            return new ThermoCoupleStatus(0, false);
        }
        
        public void SetRelayState(bool state)
        {
        }

        public void StartProfile()
        {
        }

        public void StopProfile()
        {
        }

        public int GetRoomTemp()
        {
            return 0;
        }
        public double GetThermoCoupleTemp()
        {
            return 0;
        }
        public void Ping()
        {
            SendRequest(new Request() { Command = Request.Types.RequestType.Ping });
        }
        public void Open()
        {
            _usbDevice = UsbDevice.OpenUsbDevice(_usbFinder);

            if (_usbDevice != null)
            {
                OpenUsbDevice(_usbDevice, out _usbEndpointReader, out _usbEndpointWriter);
            }
        }

        private Response SendRequest(Request request)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                request.WriteTo(stream);

                ErrorCode ec = ErrorCode.None;
                int bytesWritten;

                ec = _usbEndpointWriter.Write(stream.ToArray(), 2000, out bytesWritten);

                if (ec != ErrorCode.None)
                {
                    throw new ReflowControllerException(UsbDevice.LastErrorString);
                }

                byte[] readBuffer = new byte[1024];
                while (ec == ErrorCode.None)
                {
                    int bytesRead;

                    ec = _usbEndpointReader.Read(readBuffer, 1000, out bytesRead);

                    if (bytesRead == 0)
                    {
                        throw new ReflowControllerException(UsbDevice.LastErrorString);
                    }
                }
                return Response.Parser.ParseFrom(readBuffer);
            }
        }
        private void UsbDeviceNotifier_OnDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
            if (e.EventType == EventType.DeviceArrival)
            {
                _usbDevice = UsbDevice.OpenUsbDevice(_usbFinder);

                if (_usbDevice != null)
                {
                    OpenUsbDevice(_usbDevice, out _usbEndpointReader, out _usbEndpointWriter);

                    DeviceConnected?.Invoke(this, new EventArgs());
                }
            }

            if (e.EventType == EventType.DeviceRemoveComplete || e.EventType == EventType.DeviceRemovePending)
            {
                CloseUsbDevice(_usbDevice);

                DeviceDisconnected?.Invoke(this, new EventArgs());
            }
        }
        private static void OpenUsbDevice(UsbDevice usbDevice, out UsbEndpointReader usbEndpointReader, out UsbEndpointWriter usbEndpointWriter)
        {
            SelectDeviceInterface(usbDevice);

            usbEndpointReader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep01, 100, EndpointType.Bulk);

            usbEndpointWriter = usbDevice.OpenEndpointWriter(WriteEndpointID.Ep02, EndpointType.Bulk);
        }
        private static void SelectDeviceInterface(UsbDevice usbDevice)
        {
            IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;

            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                wholeUsbDevice.SetConfiguration(1);

                wholeUsbDevice.ClaimInterface(0);
            }
        }
        private static void CloseUsbDevice(UsbDevice usbDevice)
        {
            try
            {
                usbDevice.Close();
            }
            finally
            {
                usbDevice = null;
            }
        }

        #region IDisposable
        public void Dispose()
        {
        }

        #endregion
    }
}
