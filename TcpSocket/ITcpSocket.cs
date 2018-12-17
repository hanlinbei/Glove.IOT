using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.TcpSocket
{
    public interface ITcpSocket
    {
        void SocketInit();
        void Listen(object o);
        void Recive(object o);
    }
}
