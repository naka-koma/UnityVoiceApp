using System.Linq;
using UnityEngine;
using TMPro;
using Unity.Services.Vivox;

namespace VivoxSample
{
    public class AudioDeviceSelector : MonoBehaviour
    {
        // TODO: DIしたい
        [SerializeField] private VivoxService vivoxService;

        [SerializeField] private TMP_Dropdown inputDeviceDropdown;
        [SerializeField] private TMP_Dropdown outputDeviceDropdown;

        private void Start()
        {
            if (vivoxService)
            {
                vivoxService.AvailableInputDevicesChanged += this.OnAvailableInputDevicesChanged;
                vivoxService.AvailableOutputDevicesChanged += this.OnAvailableOutputDevicesChanged;
            }

            inputDeviceDropdown.onValueChanged += 
        }

        private void OnDestroy()
        {
            
        }

        private void UpdateAvailableInputDevices()
        {
            inputDeviceDropdown.AddOptions(vivoxService?.AvailableInputDevices.Select(v => new TMP_Dropdown.OptionData(v.DeviceName)).ToList());
            inputDeviceDropdown.SetValueWithoutNotify(vivoxService?.AvailableInputDevices.)
        }

        private void UpdateAvailableOutputDevices()
        {
            outputDeviceDropdown.AddOptions(vivoxService?.AvailableOutputDevices.Select(v => new TMP_Dropdown.OptionData(v.DeviceName)).ToList());
        }

        private void OnAvailableInputDevicesChanged()
        {
            UpdateAvailableInputDevices();
        }

        private void OnAvailableOutputDevicesChanged()
        {
            UpdateAvailableOutputDevices();
        }
    }
}