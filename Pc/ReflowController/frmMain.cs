using RJCP.IO.Ports;
using System;
using System.Windows.Forms;

namespace ReflowController
{
    public partial class frmMain : Form
    {
        private IReflowControllerDevice _reflowControllerDevice;
        private int _lastReflowTimer;

        internal frmMain()
        {
            InitializeComponent();

            _reflowControllerDevice = new ReflowControllerDeviceSerial();
        }
        
        private void SetInputControlState(bool enabled)
        {
            btnClearLog.Enabled = btnGetProfileStage.Enabled = btnGetTcStatus.Enabled = 
                btnPing.Enabled = btnRelayOff.Enabled = btnRelayOn.Enabled = 
                btnStart.Enabled = btnGetReflowProfile.Enabled = 
                btnSetProfile.Enabled = enabled;

            nudCoolRate.Enabled = nudPeakTemp.Enabled = nudSoakLength.Enabled = 
                nudSoakTemp1.Enabled = nudSoakTemp2.Enabled = nudStartRate.Enabled = 
                nudTimeToPeak.Enabled = nudPreheat.Enabled = enabled;
        }
        private void btnGetTcStatus_Click(object sender, EventArgs e)
        {
            HandleOperation("Get Thermocouple Status", () =>
            {
                var status = _reflowControllerDevice.GetThermoCoupleStatus();

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Get Thermocouple Temperature: [{status.Temperature}] Is Thermocouple Open: [{status.ThermoCoupleOpen}]\r\n");
                }));
            });
        }
        private void btnRelayOn_Click(object sender, EventArgs e)
        {
            HandleOperation("Relay On", () =>
            {
                _reflowControllerDevice.SetRelayState(true);

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Relay On: Ok\r\n");
                }));
            });
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            HandleOperation("Start Profile", () =>
            {
                _reflowControllerDevice.StartProfile();
                
                Invoke((MethodInvoker)(() =>
                {
                    StartProfileUpdate();

                    btnStop.Enabled = true;
                    btnStart.Enabled = false;
                    btnSetProfile.Enabled = false;
                    tbStatus.AppendText($"Start Profile: Ok\r\n");
                }));
            });
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            HandleOperation("Stop Profile", () =>
            {
                _reflowControllerDevice.StopProfile();

                StopProfileUpdate();

                Invoke((MethodInvoker)(() =>
                {
                    btnStop.Enabled = false;
                    btnStart.Enabled = true;
                    btnSetProfile.Enabled = true;

                    tbStatus.AppendText($"Stop Profile: Ok\r\n");
                }));
            });
        }
        private void btnRelayOff_Click(object sender, EventArgs e)
        {
            HandleOperation("Relay Off", () =>
            {
                _reflowControllerDevice.SetRelayState(false);

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Relay Off: Ok\r\n");
                }));
            });
        }
        private void btnPing_Click(object sender, EventArgs e)
        {
            HandleOperation("Ping", () =>
            {
                _reflowControllerDevice.Ping();

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"ReflowController Ping: Ok\r\n");
                }));
            });
        }
        private void btnGetProfileStage_Click(object sender, EventArgs e)
        {
            HandleOperation("Get Profile Stage", () =>
            {
                var profileStage = _reflowControllerDevice.GetProfileStage();

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Get Profile Stage - Target Temp: [{profileStage.TargetTemp * 0.25} °C] Tc Temp: [{profileStage.TcTemp * 0.25} °C] " +
                    $"State: [{profileStage.State}] Reflow Timer: [{profileStage.ReflowTimer}]\r\n");
                }));
            });
        }
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() =>
            {
                tbStatus.Text = string.Empty;
            }));
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            HandleOperation("Disconnect", () =>
            {
                _reflowControllerDevice.Close();

                Invoke((MethodInvoker)(() =>
                {
                    SetInputControlState(false);
                    btnConnect.Enabled = true;

                    tbStatus.AppendText($"Disconnect: Ok\r\n");
                }));
            });
        }
        private void btnGetReflowProfile_Click(object sender, EventArgs e)
        {
            HandleOperation("Get Reflow Profile", () =>
            {
                var reflowProfile = _reflowControllerDevice.GetReflowProfile();

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Get Reflow Profile: Ok\r\n");

                    MapFromReflowProfile(reflowProfile);

                    UpdateReflowProfileGraph();
                }));
            });
        }
        private void btnSetProfile_Click(object sender, EventArgs e)
        {
            HandleOperation("Set Reflow Profile", () =>
            {
                _reflowControllerDevice.SetReflowProfile(MapToReflowProfile());

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Set Reflow Profile: Ok\r\n");
                }));
            });
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            HandleOperation("Connect", () =>
            {
                _reflowControllerDevice.Open((string)cboPorts.SelectedItem);

                var reflowProfile = _reflowControllerDevice.GetReflowProfile();

                Invoke((MethodInvoker)(() =>
                {
                    SetInputControlState(true);
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;

                    MapFromReflowProfile(reflowProfile);

                    UpdateReflowProfileGraph();

                    tbStatus.AppendText("Connect: Ok Reflow Profile Loaded from ReflowController\r\n");
                }));
            });
        }
        private void btnGetPid_Click(object sender, EventArgs e)
        {
            HandleOperation("Get Pid", () =>
            {
                Pid pidGains = _reflowControllerDevice.GetPid();

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Get Pid: Ok\r\n");

                    nudKp.Value = pidGains.Kp;
                    nudKi.Value = pidGains.Ki;
                    nudKd.Value = pidGains.Kd;
                }));
            });
        }
        private void btnSetPid_Click(object sender, EventArgs e)
        {
            HandleOperation("Set Pid", () =>
            {
                _reflowControllerDevice.SetPid(new Pid()
                {
                    Kp = (int) nudKp.Value,
                    Ki = (int) nudKi.Value,
                    Kd = (int) nudKd.Value
                });

                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"Set Pid: Ok\r\n");
                }));
            });
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            cboPorts.Items.AddRange(SerialPortStream.GetPortNames());
        }
        private void cboPorts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnConnect.Enabled = (cboPorts.SelectedItem != null);
        }
        private void tmrMain_Tick(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() =>
            {
                var profileStage = _reflowControllerDevice.GetProfileStage();
                
                _lastReflowTimer = profileStage.ReflowTimer;

                chartMain.Series[1].Points.AddXY(profileStage.ReflowTimer, profileStage.TcTemp * 0.25);
                chartMain.Series[2].Points.AddXY(profileStage.ReflowTimer, profileStage.TargetTemp * 0.25);

                tbStatus.AppendText(string.Format("Get Profile Stage - Target Temp: [{0:N2} °C] Tc Temp: [{1:N2}] °C " +
                    "State: [{2}] Reflow Timer: [{3}] Duty Cycle: [{4}]\r\n", 
                    profileStage.TargetTemp * 0.25,
                    profileStage.TcTemp * 0.25,
                    profileStage.State,
                    profileStage.ReflowTimer,
                    profileStage.DutyCycle));
            }));
        }
        private void StartProfileUpdate()
        {
            chartMain.Series[0].Points.Clear();
            chartMain.Series[1].Points.Clear();
            chartMain.Series[2].Points.Clear();
            _lastReflowTimer = 0;
            
            tmrMain.Start();
        }
        private void StopProfileUpdate()
        {
            tmrMain.Stop();
        }
        private void UpdateReflowProfileGraph()
        {
            chartMain.Series[0].Points.Clear();
            
            var soakStart = ((nudSoakTemp1.Value - nudPreheat.Value) / nudStartRate.Value);
            var soakEnd = soakStart + nudSoakLength.Value;
            var peak = soakEnd + nudTimeToPeak.Value;

            chartMain.Series[0].Points.AddXY(0, nudPreheat.Value);
            chartMain.Series[0].Points.AddXY(soakStart, nudSoakTemp1.Value);
            chartMain.Series[0].Points.AddXY(soakEnd, nudSoakTemp2.Value);
            chartMain.Series[0].Points.AddXY(peak, nudPeakTemp.Value);
            chartMain.Series[0].Points.AddXY(peak + (nudPeakTemp.Value / nudCoolRate.Value), 0);
            chartMain.Invalidate();
        }

        private void HandleOperation(string operationName, Action operation)
        {
            HandleOperation(operationName, operation, (e) => { });
        }

        private void HandleOperation(string operationName, Action operation, Action<Exception> errorOperation)
        {
            try
            {
                operation();
            }
            catch (ReflowControllerException ex)
            {
                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"ReflowController {operationName}: [{ex.Message}]\r\n");
                }));

                errorOperation(ex);
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() =>
                {
                    tbStatus.AppendText($"ReflowController {operationName}: [{ex.ToString()}]\r\n");
                }));

                errorOperation(ex);
            }
        }
        private void MapFromReflowProfile(ReflowProfile reflowProfile)
        {
            nudPreheat.Value = reflowProfile.Preheat / 4;
            nudCoolRate.Value = reflowProfile.CoolRate / 4;
            nudPeakTemp.Value = reflowProfile.PeakTemp / 4;
            nudSoakLength.Value = reflowProfile.SoakLen;
            nudSoakTemp1.Value = reflowProfile.SoakTemp1 / 4;
            nudSoakTemp2.Value = reflowProfile.SoakTemp2 / 4;
            nudStartRate.Value = reflowProfile.StartRate / 4;
            nudTimeToPeak.Value = reflowProfile.TimeToPeak;
        }
        private void Profile_ValueChanged(object sender, EventArgs e)
        {
            UpdateReflowProfileGraph();
        }
        private ReflowProfile MapToReflowProfile()
        {
            return new ReflowProfile()
            {
                Preheat = (int) nudPreheat.Value * 4,
                CoolRate = (int) nudCoolRate.Value * 4,
                PeakTemp = (int) nudPeakTemp.Value * 4,
                SoakLen = (int) nudSoakLength.Value,
                SoakTemp1 = (int) nudSoakTemp1.Value * 4,
                SoakTemp2 = (int) nudSoakTemp2.Value * 4,
                StartRate = (int) nudStartRate.Value * 4,
                TimeToPeak = (int) nudTimeToPeak.Value
            };
        }
    }
}
