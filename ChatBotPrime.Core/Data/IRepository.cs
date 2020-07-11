using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Core.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Data
{
    public interface IRepository
    {
        T Single<T>(ISpecification<T> spec) where T : DataEntity;
        List<T> List<T>(ISpecification<T> spec = null) where T : DataEntity;
        T Create<T>(T dataItem) where T : DataEntity;
        T Update<T>(T dataItem) where T : DataEntity;
        void Update<T>(List<T> dataItemList) where T : DataEntity;
        void Create<T>(List<T> dataItemList) where T : DataEntity;
        void Remove<T>(T dataItem) where T : DataEntity;
        void Remove<T>(List<T> dataItems) where T : DataEntity;
    }
}
