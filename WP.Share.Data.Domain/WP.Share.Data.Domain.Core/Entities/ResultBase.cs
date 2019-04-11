using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.Entities
{
    public class ResultBase<T> : IResultBase
    {
        public bool IsError { get; set; }
        public string Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
        public ResultLayerEnum ResultLayer { get; set; }

    }
    public interface IResultBase
    {
        bool IsError { get; set; }
        string Code { get; set; }
        string Msg { get; set; }
        ResultLayerEnum ResultLayer { get; set; }
    }
    public enum ResultLayerEnum
    {
        DAO,
        Manager,
        Service
    }
}
