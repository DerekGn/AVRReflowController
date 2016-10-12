using Google.Protobuf;
using RJCP.IO.Ports;
using System;
using System.IO;
using System.Linq;

namespace ReflowController
{
    internal class ReflowControllerDeviceSerial : IReflowControllerDevice, IDisposable
    {
        private const int TcInputStateMask = 0x4;
        private object _lock = new object();
        
        private SerialPortStream _serialPortStream;
        private SerialError _lastError;
        private SerialData _serialData;

        public ThermoCoupleStatus GetThermoCoupleStatus()
        {
            var response = SendRequest(new Request() { Command = Request.Types.RequestType.Tcstate });

            var isOpen = (response.TcState & TcInputStateMask) > 0;
            var temp = (response.TcState >> 3) * 0.25;

            return new ThermoCoupleStatus(temp, isOpen);
        }
        
        public void Ping()
        {
            SendRequest(new Request() { Command = Request.Types.RequestType.Ping });
        }

        public void SetRelayState(bool state)
        {
            SendRequest(new Request() { Command = state ? Request.Types.RequestType.Relayon : Request.Types.RequestType.Relayoff });
        }

        public void StartProfile()
        {
            SendRequest(new Request() { Command = Request.Types.RequestType.Startprofile });
        }

        public void StopProfile()
        {
            SendRequest(new Request() { Command = Request.Types.RequestType.Stopprofile });
        }
        public ProfileStage GetProfileStage()
        {
            return SendRequest(new Request() { Command = Request.Types.RequestType.Getprofilestage }).Stage;
        }
        public ReflowProfile GetReflowProfile()
        {
            return SendRequest(new Request() { Command = Request.Types.RequestType.Getprofile }).Profile;
        }

        public void SetReflowProfile(ReflowProfile reflowProfile)
        {
            SendRequest(new Request() { Command = Request.Types.RequestType.Setprofile, Profile = reflowProfile });
        }

        public void Open(string port)
        {
            if (!SerialPortStream.GetPortNames().Contains(port))
            {
                throw new ReflowControllerException($"Port does not exist: {port}" );
            }

            if (_serialPortStream == null)
            {
                _serialPortStream = new SerialPortStream(port);
                _serialPortStream.Open();
            }
            else
                throw new ReflowControllerException("Device aplready open");
        }
        
        public void Close()
        {
            lock (_lock)
            {
                if (_serialPortStream != null)
                {
                    _serialPortStream.Close();
                    _serialPortStream.Dispose();
                    _serialPortStream = null;
                }
            }
        }
        
        private Response SendRequest(Request request)
        {
            lock(_lock)
            {
                var requestBytes = request.ToByteArray();

                _serialPortStream.Write(requestBytes, 0, requestBytes.Length);

                byte[] readBuffer = new byte[64];
                int readLen = 0;

                if (_lastError == SerialError.NoError)
                {
                    readLen = _serialPortStream.Read(readBuffer, 0, 64);
                }
                else
                {
                    throw new ReflowControllerException(_lastError.ToString());
                }

                Response response = Deserialise(readBuffer, readLen);

                if (response.Result == Response.Types.ResultType.Fail)
                {
                    throw new ReflowControllerException($"ReflowController Error Code: {response.ErrorCode}");
                }

                return response;
            }
        }

        private Response Deserialise(byte[] readBuffer, int readLen)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(readBuffer, 0, readLen);
                stream.Seek(0, SeekOrigin.Begin);

                return Response.Parser.ParseFrom(stream);
            }
        }

        private void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _lastError = e.EventType;
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _serialData = e.EventType;
        }

        private void PinChanged(object sender, SerialPinChangedEventArgs e)
        {
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        #endregion
    }
}
