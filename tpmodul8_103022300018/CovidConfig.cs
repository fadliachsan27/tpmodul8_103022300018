using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpmodul8_103022300018
{
    public class CovidConfig
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        private const string ConfigPath = "covid_config.json";

        public CovidConfig()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                var config = JsonConvert.DeserializeObject<CovidConfigData>(json);

                satuan_suhu = config.satuan_suhu ?? "celcius";
                batas_hari_deman = config.batas_hari_deman == 0 ? 14 : config.batas_hari_deman;
                pesan_ditolak = config.pesan_ditolak ?? "Anda tidak diperbolehkan masuk ke dalam gedung ini";
                pesan_diterima = config.pesan_diterima ?? "Anda dipersilahkan untuk masuk ke dalam gedung ini";
            }
            else
            {
                // Set default
                satuan_suhu = "celcius";
                batas_hari_deman = 14;
                pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
                pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
                SaveConfig();
            }
        }

        public void UbahSatuan()
        {
            if (satuan_suhu == "celcius")
            {
                satuan_suhu = "fahrenheit";
            }
            else
            {
                satuan_suhu = "celcius";
            }
            SaveConfig();
        }

        public void SaveConfig()
        {
            var config = new CovidConfigData
            {
                satuan_suhu = this.satuan_suhu,
                batas_hari_deman = this.batas_hari_deman,
                pesan_ditolak = this.pesan_ditolak,
                pesan_diterima = this.pesan_diterima
            };

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(ConfigPath, json);
        }
    }

    // POCO class untuk deserialisasi JSON
    public class CovidConfigData
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }
    }
}
