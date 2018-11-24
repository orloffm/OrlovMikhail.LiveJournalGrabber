using System;
using System.Collections;
using System.Collections.Generic;

namespace OrlovMikhail.LJ.Grabber.Tools
{
    public sealed class TreeNode<T> : IEnumerable<TreeNode<T>>, IEquatable<TreeNode<T>>
    {
        public TreeNode(T data, TreeNode<T> parent = null)
        {
            Data = data;
            Parent = parent;
            Children = new LinkedList<TreeNode<T>>();
        }

        public ICollection<TreeNode<T>> Children { get; }

        public T Data { get; set; }

        public bool IsLeaf => Children.Count == 0;

        public bool IsRoot => Parent == null;

        public int Level
        {
            get
            {
                if (IsRoot)
                {
                    return 0;
                }

                return Parent.Level + 1;
            }
        }

        public TreeNode<T> Parent { get; private set; }

        public TreeNode<T> AddChild(T child)
        {
            var childNode = new TreeNode<T>(child) {Parent = this};
            Children.Add(childNode);

            return childNode;
        }

        public override string ToString()
        {
            return Data != null ? Data.ToString() : "[data null]";
        }

        #region iterating

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            yield return this;
            foreach (TreeNode<T> directChild in Children)
            {
                foreach (TreeNode<T> anyChild in directChild)
                {
                    yield return anyChild;
                }
            }
        }

        #endregion

        #region equality

        public bool Equals(TreeNode<T> other)
        {
            return ReferenceEquals(this, other);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        #endregion
    }
}