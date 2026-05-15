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

        //AUTENTICAÇÃO
        builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
        //LOGIN
        builder.RegisterType<LoginForm>().As<ILoginView>().AsSelf().SingleInstance();
        builder.RegisterType<LoginPresenter>().As<ILoginPresenter>().AsSelf().AutoActivate().SingleInstance();
        //MAINFORM
        builder.RegisterType<MainForm>().As<IMainView>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<MainPresenter>().As<IMainPresenter>().InstancePerLifetimeScope();

        var container = builder.Build();

        using (var scope = container.BeginLifetimeScope())
        {
            var loginForm = scope.Resolve<LoginForm>();
            Application.Run(loginForm);

            var authService = scope.Resolve<IAuthService>();
            if (authService.IsAutenticado)
            {
                var mainForm = scope.Resolve<MainForm>();
                var mainPresenter = scope.Resolve<IMainPresenter>();

                mainPresenter.Inicializar(); // Carrega o WhatsApp Web
                Application.Run(mainForm);
            }
        }
    }
}