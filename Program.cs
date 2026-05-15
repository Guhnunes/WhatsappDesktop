using Autofac;
using System;
using System.Windows.Forms;
using WhatsappDesktop;
using WhatsappDesktop.Features.Infraestrutura;
using WhatsappDesktop.Features.Login.Presenter;
using WhatsappDesktop.Features.Login.View;

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
        builder.RegisterType<LoginForm>().As<ILoginView>().AsSelf().InstancePerLifetimeScope();
        //PRESENTER
        builder.RegisterType<LoginPresenter>().As<ILoginPresenter>().AutoActivate();

        var container = builder.Build();

        using (var scope = container.BeginLifetimeScope())
        {
            // Resolve o formulário dentro do escopo
            var loginForm = scope.Resolve<LoginForm>();

            // Garante que o Windows não tente "pintar" o form antes da hora
            Application.Run(loginForm);
        }
    }
}