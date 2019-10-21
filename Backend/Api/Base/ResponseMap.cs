using System.Collections.Generic;
using System.Xml;
using Backend.Utility;
using Core.Extensions;

namespace Backend.Api.Base
{
    public sealed class ResponseMap : Dictionary<string, string>, IList<ResponseMap>
    {
        private XmlDocument Document { get; }
        public List<ResponseMap> Responses { get; private set; } = new List<ResponseMap>();

        public ResponseMap() : base() => Responses.Add(this);

        public ResponseMap(XmlDocument document)
        {
            Document = document;
            Responses.Add(this);
        }

        public ResponseMap(IEqualityComparer<string> comparer) : base(comparer) => Responses.Add(this);

        public ResponseMap(IDictionary<string, string> dictionary) : base(dictionary) => Responses.Add(this);

        public Dictionary<string, string> LocateValues(XmlDocument document = null)
        {
            Keys.ForEach(key => this[key] = Search.Document(document ?? Document).ByXpath(this[key]));
            return this;
        }

        public void Add(ResponseMap item) => Responses.Add(item);

        public bool Contains(ResponseMap item) => Responses.Contains(item);

        public void CopyTo(ResponseMap[] array, int arrayIndex) => Responses.CopyTo(array, arrayIndex);

        public bool Remove(ResponseMap item) => Responses.Remove(item);

        public bool IsReadOnly { get; } = false;
        public int IndexOf(ResponseMap item) => Responses.IndexOf(item);

        public void Insert(int index, ResponseMap item) => Responses.Insert(index, item);

        public void RemoveAt(int index) => Responses.RemoveAt(index);

        public ResponseMap this[int index]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public new IEnumerator<ResponseMap> GetEnumerator() => ((IEnumerable<ResponseMap>) Responses).GetEnumerator();

        public override string ToString()
        {
            return $"{nameof(Document)}: {Document}, {nameof(Responses)}: {Responses}, " +
                   $"{nameof(IsReadOnly)}: {IsReadOnly}";
        }
    }
}