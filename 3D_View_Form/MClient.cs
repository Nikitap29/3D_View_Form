using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace _3D_View_Form
{
    /// <summary>
    /// Подключение к брокеру MQTT
    /// </summary>
    public class MClient
    {
        /// <summary>
        /// Объект для подключения к брокеру
        /// </summary>
        private IMqttClient mqtt;

        /// <summary>
        /// IP-адрес брокера
        /// </summary>
        public string IPaddress
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Список топиков, опрашиваемых классом
        /// </summary>
        public List<string> Topics
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// ID-клиента MQTT
        /// </summary>
        public string ClientID
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Подключение к брокеру
        /// </summary>
        public async void Connect()
        {
            LoadJSON();
            mqtt = new MqttFactory().CreateMqttClient(); //создаем клиент
            var opt = new MqttClientOptionsBuilder() //определяем опции
                    .WithClientId(ClientID) //ID клиента
                    .WithTcpServer(IPaddress, Port) //адрес сервера
                                                    //.WithCredentials("makino", "onikam") //аутентификация
                    .Build();
            await mqtt.ConnectAsync(opt, CancellationToken.None);
        }

        /// <summary>
        /// Загрузка свойств из JSON
        /// </summary>
        private int LoadJSON()
        {
            int res = 0;
            string s;
            if (File.Exists("mqtt"))
            {
                s = File.ReadAllText("mqtt");
            }
            else
            {
                return -1;
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            MClient m;
            try
            {
                m = JsonSerializer.Deserialize<MClient>(s, options);
            }
            catch
            {
                return -1;
            }
            ClientID = m.ClientID;
            IPaddress = m.IPaddress;
            Port = m.Port;
            Topics = m.Topics;
            return 0;
        }

        /// <summary>
        /// Сохранение свойств в JSON
        /// </summary>
        public void SaveJSON(bool def)
        {
            if (def)
            {
                ClientID = "Client77";
                IPaddress = "localhost";
                Port = 1883;
                Topics.Add("topic1");
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string s = JsonSerializer.Serialize(this, options);
            File.WriteAllText("mqtt", s);
        }
    }
}