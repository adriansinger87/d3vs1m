using D3vS1m.Application.Data;
using D3vS1m.Domain.Data.Arguments;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Settings;
using System;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class SphericAntennaArgs : ArgumentsBase
    {
        public SphericAntennaArgs() : base()
        {
            Key = Models.Antenna.Spheric.Key;
            Name = Models.Antenna.Spheric.Name;
            Reset();
        }

        public override void Reset()
        {
           
        }

        public void LoadData(IPersistenceControlable io, SettingsBase setting, string key)
        {
            GainMatrix = io.Importer(key).Setup(setting).Import()
                .As<Matrix<SphericGain>>(new TableToAntennaAdapter());
        }

        public Matrix<SphericGain> GainMatrix { get; set; }

        public string DataSource { get; set; }
    }
}
