using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure.SingleLinkedList
{
    public class DbNode<T>
    {
        private T Item { get; set; }
        public DbNode<T> NextNode { get; set; }
        public DbNode<T> PreNode { get; set; }
        public DbNode()
        { }
        public DbNode(T value)
        {
            this.Item = value;
        }
    }

    public class BaconDoubleLinkedList<T>
    {
        private int count;
        private DbNode<T> head;
        public BaconDoubleLinkedList()
        {
            this.count = 0;
            this.head = null;
        }
        public DbNode<T> GetDbNodeByIndex(int index)
        {
            if (index < 0 || index > count)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            DbNode<T> tempDbNode = this.head;
            for (int i = 0; i < index; i++)
            {
                tempDbNode = tempDbNode.NextNode;
            }
            return tempDbNode;
        }
        public void Add(T value)
        {
            DbNode<T> newNode = new DbNode<T>(value);
            if (this.head == null)
            {
                // 如果链表当前为空则置为头结点
                this.head = newNode;
            }
            else
            {

            }
        }
    }
}
