using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceEndpoint.Models
{
    public enum DeviceType
    {
        presenceSensor,
        temperatureSensor,
        brightnessSensor,
        atmosphericPressureSensor,
        humiditySensor,
        soundLevelSensor,
        gpsSensor,
        co2Sensor,
        ledDevice,
        beeperDevice,
    }
}
