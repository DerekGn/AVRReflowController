using System;

namespace ReflowController
{
    /// <summary>
    /// The definition of an <see cref="IReflowControllerDevice"/>
    /// </summary>
    internal interface IReflowControllerDevice
    {
        /// <summary>
        /// Open the device
        /// </summary>
        /// <param name="port">The COM port that the reflow controller is connected</param>
        void Open(string port);
        void Close();
        ThermoCoupleStatus GetThermoCoupleStatus();
        void SetRelayState(bool state);
        void StartProfile();
        void StopProfile();
        ProfileStage GetProfileStage();
        void Ping();
        ReflowProfile GetReflowProfile();
        void SetReflowProfile(ReflowProfile reflowProfile);
        Pid GetPid();
        void SetPid(Pid pid);
    }
}