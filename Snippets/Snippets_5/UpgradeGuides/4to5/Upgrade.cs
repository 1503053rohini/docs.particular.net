namespace Snippets5.UpgradeGuides._4to5
{
    using System;
    using NServiceBus;
    using NServiceBus.Persistence;
    using Raven.Client.Document;

    public class Upgrade
    {
        public void MessageConventions()
        {
            #region 4to5MessageConventions

            BusConfiguration busConfiguration = new BusConfiguration();
            ConventionsBuilder conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace == "MyNamespace" && t.Namespace.EndsWith("Commands"));
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace == "MyNamespace" && t.Namespace.EndsWith("Events"));
            conventions.DefiningMessagesAs(t => t.Namespace != null && t.Namespace == "Messages");
            conventions.DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
            conventions.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
            conventions.DefiningExpressMessagesAs(t => t.Name.EndsWith("Express"));
            conventions.DefiningTimeToBeReceivedAs(t => t.Name.EndsWith("Expires") ? TimeSpan.FromSeconds(30) : TimeSpan.MaxValue);

            #endregion
        }

        public void CustomConfigOverrides()
        {
            #region 4to5CustomConfigOverrides

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.AssembliesToScan(AllAssemblies.Except("NotThis.dll"));
            busConfiguration.Conventions().DefiningEventsAs(type => type.Name.EndsWith("Event"));
            busConfiguration.EndpointName("MyEndpointName");

            #endregion
        }

        public void InterfaceMessageCreation()
        {
            IBus Bus = null;

            #region 4to5InterfaceMessageCreation

            Bus.Publish<MyInterfaceMessage>(o =>
            {
                o.OrderNumber = 1234;
            });

            #endregion

            IMessageCreator messageCreator = null;

            #region 4to5ReflectionInterfaceMessageCreation

            //This type would be derived from some other runtime information
            Type messageType = typeof(MyInterfaceMessage);

            object instance = messageCreator.CreateInstance(messageType);

            //use reflection to set properties on the constructed instance

            Bus.Publish(instance);

            #endregion
        }

        public interface MyInterfaceMessage
        {
            int OrderNumber { get; set; }
        }

        public void CustomRavenConfig()
        {
            #region 4to5CustomRavenConfig

            DocumentStore documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "MyDatabase",
            };

            documentStore.Initialize();

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.UsePersistence<RavenDBPersistence>()
                .SetDefaultDocumentStore(documentStore);

            #endregion
        }

        public void StartupAction()
        {
            #region 4to5StartupAction

            IStartableBus bus = Bus.Create(new BusConfiguration());
            MyCustomAction();
            bus.Start();

            #endregion
        }

        public void MyCustomAction()
        {

        }

        public void Installers()
        {
            #region 4to5Installers

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.EnableInstallers();

            Bus.Create(busConfiguration); //this will run the installers

            #endregion
        }

        public void AllThePersistence()
        {
#pragma warning disable 618

            #region 4to5ConfigurePersistence

            BusConfiguration busConfiguration = new BusConfiguration();

            // Configure to use InMemory for all persistence types
            busConfiguration.UsePersistence<InMemoryPersistence>();

            // Configure to use InMemory for specific persistence types
            busConfiguration.UsePersistence<InMemoryPersistence>()
                .For(Storage.Sagas, Storage.Subscriptions);

            // Configure to use NHibernate for all persistence types
            busConfiguration.UsePersistence<NHibernatePersistence>();

            // Configure to use NHibernate for specific persistence types
            busConfiguration.UsePersistence<NHibernatePersistence>()
                .For(Storage.Sagas, Storage.Subscriptions);

            // Configure to use RavenDB for all persistence types
            busConfiguration.UsePersistence<RavenDBPersistence>();

            // Configure to use RavenDB for specific persistence types
            busConfiguration.UsePersistence<RavenDBPersistence>()
                .For(Storage.Sagas, Storage.Subscriptions);

            #endregion

#pragma warning restore 618
        }

        #region 4to5BusExtensionMethodForHandler

        public class MyHandler : IHandleMessages<MyMessage>
        {
            IBus bus;

            public MyHandler(IBus bus)
            {
                this.bus = bus;
            }

            public void Handle(MyMessage message)
            {
                bus.Reply(new OtherMessage());
            }
        }

        #endregion

        public class MyMessage
        {
        }

        public class OtherMessage
        {
        }

        public void RunCustomAction()
        {
            #region 4to5RunCustomAction

            IStartableBus bus = Bus.Create(new BusConfiguration());
            MyCustomAction();
            bus.Start();

            #endregion
        }

        public void DefineCriticalErrorAction()
        {

            #region 4to5DefineCriticalErrorAction

            BusConfiguration busConfiguration = new BusConfiguration();

            // Configuring how NServicebus handles critical errors
            busConfiguration.DefineCriticalErrorAction((message, exception) =>
            {
                string output = string.Format("We got a critical exception: '{0}'\r\n{1}", message, exception);
                Console.WriteLine(output);
                // Perhaps end the process??
            });

            #endregion
        }

        public void FileShareDataBus()
        {
            string databusPath = null;

            #region 4to5FileShareDataBus

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.UseDataBus<FileShareDataBus>()
                .BasePath(databusPath);

            #endregion
        }

        public void PurgeOnStartup()
        {
            #region 4to5PurgeOnStartup

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.PurgeOnStartup(true);

            #endregion
        }

        public void EncryptionServiceSimple()
        {
            #region 4to5EncryptionServiceSimple

            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.RijndaelEncryptionService();

            #endregion
        }

        public void License()
        {
            #region 4to5License

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.LicensePath("PathToLicense");
            //or
            busConfiguration.License("YourCustomLicenseText");

            #endregion
        }

        public void TransactionConfig()
        {
            #region 4to5TransactionConfig

            BusConfiguration busConfiguration = new BusConfiguration();

            //Enable
            busConfiguration.Transactions().Enable();

            // Disable
            busConfiguration.Transactions().Disable();

            #endregion
        }

        public void StaticConfigureEndpoint()
        {
            #region 4to5StaticConfigureEndpoint

            BusConfiguration busConfiguration = new BusConfiguration();

            // SendOnly
            Bus.CreateSendOnly(busConfiguration);

            // AsVolatile
            busConfiguration.Transactions().Disable();
            busConfiguration.DisableDurableMessages();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            // DisableDurableMessages
            busConfiguration.DisableDurableMessages();

            // EnableDurableMessages
            busConfiguration.EnableDurableMessages();

            #endregion
        }

        public void PerformanceMonitoring()
        {
            #region 4to5PerformanceMonitoring

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.EnableSLAPerformanceCounter();
            //or
            busConfiguration.EnableSLAPerformanceCounter(TimeSpan.FromMinutes(3));

            #endregion
        }

        public void DoNotCreateQueues()
        {
            #region 4to5DoNotCreateQueues

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.DoNotCreateQueues();

            #endregion
        }

        public void EndpointName()
        {
            #region 4to5EndpointName

            BusConfiguration busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName("MyEndpoint");

            #endregion
        }

        public void SendOnly()
        {
            #region 4to5SendOnly

            BusConfiguration busConfiguration = new BusConfiguration();

            ISendOnlyBus bus = Bus.CreateSendOnly(busConfiguration);

            #endregion
        }
    }
}