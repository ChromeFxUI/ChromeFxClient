using Models;
using ReduxCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeFxClient
{
    public static class NoticeMiddleware
    {
        public static void UseNoticeMiddleware(this Package<Store> store)
        {
            store.Middleware(
                package => next => action =>
                {
                    next(action);
                }
                );
        }
    }
}
