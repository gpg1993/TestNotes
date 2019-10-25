using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure.SingleLinkedList
{
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }
        public Node()
        {

        }
        public Node(T item)
        {
            this.Item = item;
        }
    }

    public class BaconSingleLinkedList<T>
    {
        private int count;
        private Node<T> head;
        public int Count
        {
            get
            {
                return this.count;
            }
        }
        public T this[int index]
        {
            get
            {
                return this.GetNodeByIndex(index).Item;
            }
            set
            {
                this.GetNodeByIndex(index).Item = value;
            }
        }
        public BaconSingleLinkedList()
        {
            this.count = 0;
            this.head = null;
        }
        private Node<T> GetNodeByIndex(int index)
        {
            if (index<0|| index>count)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            Node<T> tempNode = this.head;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }
        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (this.head == null)
            {
                // 如果链表当前为空则置为头结点
                this.head = newNode;
            }
            else
            {
                Node<T> preNode = this.GetNodeByIndex(this.count - 1);
                preNode.Next = newNode;
            }
            this.count++;
        }
        public void Insert(int index,T value)
        {
            Node<T> tempNode = null;
            if (index < 0 || index > this.count)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            else if(index==0)
            {
                if (this.head==null)
                {
                    tempNode = new Node<T>(value);
                    head = tempNode;
                }
                else
                {
                    tempNode = new Node<T>(value);
                    tempNode.Next = head;
                    this.head = tempNode;
                }
            }
            else
            {
                Node<T> preNode = this.GetNodeByIndex(index - 1);
                tempNode = new Node<T>(value);
                tempNode.Next = preNode.Next;
                preNode.Next = tempNode;
            }
            count++;
        }
        public void RemoveAt(int index)
        {
            if (index==0)
            {
                this.head = this.head.Next;
            }
            else
            {
                Node<T> prevNode = GetNodeByIndex(index - 1);
                if (prevNode.Next == null)
                {
                    throw new ArgumentOutOfRangeException("index", "索引超出范围");
                }
                Node<T> delNode = prevNode.Next;
                prevNode.Next = delNode.Next;
                delNode = null;
            }
            this.count--;
        }
    }
}
