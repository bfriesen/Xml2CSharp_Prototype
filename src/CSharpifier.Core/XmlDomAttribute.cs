﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CSharpifier
{
    public class XmlDomAttribute : IDomElement
    {
        private readonly XAttribute _attribute;

        public XmlDomAttribute(XAttribute attribute)
        {
            _attribute = attribute;
        }

        public bool HasElements
        {
            get { return false; }
        }

        public string Value
        {
            get { return _attribute.Value; }
        }

        public string Name
        {
            get { return _attribute.Name.ToString(); }
        }

        public IEnumerable<IDomElement> Elements
        {
            get
            {
                yield break;
            }
        }

        public Property CreateProperty(IClassRepository classRepository)
        {
            var property = new Property(_attribute.Name);
            property.AppendPotentialPropertyDefinitions(
                BclClass.All
                    .Select(bclClass =>
                        new PropertyDefinition(bclClass, _attribute.Name.ToString())
                        {
                            Attributes = new List<AttributeProxy> { AttributeProxy.XmlAttribute(_attribute.Name.ToString()) },
                            IsLegal = bclClass.IsLegalValue(_attribute.Value)
                        }));
            return property;
        }
    }

}