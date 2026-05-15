using Autofac;
using System;
using System.Windows.Forms;
using WhatsappDesktop;
using WhatsappDesktop.Features.Infraestrutura;
using WhatsappDesktop.Features.Login.Presenter;
using WhatsappDesktop.Features.Login.View;
using WhatsappDesktop.Features.Main.Presenter;
using WhatsappDesktop.Features.Main.View;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var builder = new ContainerBuilder();


        builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();

        builder.RegisterType<LoginForm>().As<ILoginView>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<LoginPresenter>().As<ILoginPresenter>().AsSelf().AutoActivate();

        builder.RegisterType<MainForm>().As<IMainView>().AsSelf().SingleInstance();
        builder.RegisterType<MainPresenter>().As<IMainPresenter>().SingleInstance();

        var container = builder.Build();

        using (var scope = container.BeginLifetimeScope())
        {
            var presenter = scope.Resolve<ILoginPresenter>();

            var loginForm = (LoginForm)scope.Resolve<ILoginView>();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var authService = scope.Resolve<IAuthService>();
                if (authService.IsAutenticado)
                {
                    var mainPresenter = scope.Resolve<IMainPresenter>();
                    var mainForm = (MainForm)scope.Resolve<IMainView>();

                    mainPresenter.Inicializar();
                    Application.Run(mainForm);
                }
            }
        }
    }
}