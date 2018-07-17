using NUnit.Framework;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void GetNodeByNameReturnsNodeWhenNested()
        {
            var name = "here it is";
            var nodeToFind = new Node { Name = name, Value = "node value" };

            Node root = new Node
            {
                Children =
                {
                    new Node {Name = "l1a"},
                    new Node {Name = "l1b"},
                    new Node {Name = "l1c"},
                    new Node {Name = "l1d"},
                    new Node
                    {
                        Name = "l1e",
                        Children =
                        {
                            new Node {Name = "l2a"},
                            new Node {Name = "l2b"},
                            new Node {Name = "l2c"},
                            new Node {Name = "l2d"},
                            new Node
                            {
                                Name = "l2e",
                                Children = {nodeToFind}
                            }
                        }
                    }
                }
            };

            var nodeByName = root.GetNodeByName(name);

            Assert.AreEqual(nodeToFind, nodeByName);
        }

        [Test]
        public void GetNodeByNameReturnsNullIfItDoesNotExist()
        {
            var name = "total";

            Node root = new Node
            {
                Children =
                {
                    new Node
                    {
                        Name = "expense",
                        Children =
                        {
                            new Node()
                            {
                                Name = "total_invalid",
                            },
                            new Node()
                            {
                                Name = "cost_center"
                            },
                            new Node()
                            {
                                Name = "payment_method"
                            }
                        }
                    }
                }
            };

            var nodeByName = root.GetNodeByName(name);

            Assert.IsNull(nodeByName);
        }
    }
}
