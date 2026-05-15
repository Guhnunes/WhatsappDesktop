using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public abstract class BasePresenter<TView>
    {
        protected TView View;
        protected BasePresenter(TView view) => View = view;
        public virtual void SetView(TView view) => View = view;
    }
}
