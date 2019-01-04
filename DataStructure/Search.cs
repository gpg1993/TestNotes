using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure
{
    public class Search
    {
        #region StaticSearch 静态查询
        /// <summary>
        /// 线性结构，所以也被称为线性查找(Linear Search)，从顺序表的一端向另一端逐个扫描，找到要的记录就返回其位置，找不到则返回失败信息(通常为-1)。
        /// </summary>
        /// <param name="arr">要查找的顺序表(比如数组)</param>
        /// <param name="key">要查找的值</param>
        /// <returns>找到返回元素的下标+1，否则返回-1</returns>
        public static int SeqSearch(int[] arr, int key)
        {
            int i = -1;
            if (arr == null|| arr.Length<=0)
                return i;
            arr[0] = key;//第一个元素约定用来存放要查找的值，这个位置也称为“监视哨”，当然这并不是必须的，只是为了遵守原书的约定而已(以下同)
            bool flag = false;
            for (int a = 1; a < arr.Length; a++)
            {
                if (arr[a]==key)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)  i = -1;
            return i;
        }

        /// <summary>
        /// 二分查找(适用于有序表)查找表本身是有序的(比如从小到大排列)，所以不必傻傻的遍历每个元素，可以取中间的元素与要查找的值比较，
        /// 比如查找值大于中间元素，则要查找的元素肯定在后半段；反之如果查找值小于中间元素，则要查找的元素在前半段；然后继续二分，如此反复处理，直到找到要找的元素。
        /// </summary>
        /// <param name="arr">要查找的有序表</param>
        /// <param name="key">要查找的值</param>
        /// <returns>找到则返回元素的下标+1，否则返回-1</returns>
        static int BinarySearch(int[] arr, int key)
        {
            arr[0] = key;//同样约定第一个位置存放要查找的元素值(仅仅只是约定而已)
            int mid = 0;
            int flag = -1;
            int low = 1;
            int high = arr.Length - 1;

            while (low <= high)
            {
                //取中点
                mid = (low + high) / 2;

                //查找成功，记录位置存放到flag中
                if (key == arr[mid])
                {
                    flag = mid;
                    break;
                }
                else if (key < arr[mid]) //调整到左半区
                {
                    high = mid - 1;
                }
                //调整到右半区
                else
                {
                    low = mid + 1;
                }
            }

            if (flag > 0)
            {
                return flag;//找到了
            }
            else
            {
                return -1;//没找到
            }
        }

        //索引查找
        //思路：可以在查找表中选取一些关键记录，创建一个小型的有序表（该表中的每个元素除了记录自身值外，还记录了对应主查找表中的位置），即索引表。
        //查找时，先到索引表中通过索引记录大致判断要查找的记录在主表的哪个区域，然后定位到主表的相应区域中，仅搜索这一个区块即可。
        //因为索引表本身是有序的，所以查找索引表时，可先用前面提到的二分查找判断大致位置，然后定位到主表中，用顺序查找。
        //比如：要查找值为78的记录，先到索引表中二分查找，能知道该记录，应该在主表索引13至18 之间（即第4段），然后定位到主表中的第4段顺序查找，
        //如果找不到，则返回-1，反之则返回下标。
        //所以该方法的关键在于索引的建立！以上图为例，在主表中挑选关键值创建索引时，要求该关键值以前的记录都比它小，这样创建的索引表才有意义。
        //其实该思路在很多产品中都有应用，比如数据库的索引以及Lucene.Net都可以看作索引查找的实际应用。
        //顺便提一下：如果查找主表记录超级多，达到海量的级别，最终创建的索引表记录仍然很多，这样二分法查找还是比较慢，
        //这时可以在索引表的基础上再创建一个索引的索引，称之为二级索引，如果二级索引仍然记录太多，可以再创建三级索引。
        #endregion

        #region DynamicSearch 动态查询


        #endregion

    }

    public class Node<T>
    {
        private T data;
        private Node<T> lChild;//左子节点
        private Node<T> rChild;//右子节点

        public Node(T data, Node<T> ln, Node<T> rn)
        {
            this.data = data;
            this.lChild = ln;
            this.rChild = rn;
        }

        public Node(Node<T> ln, Node<T> rn)
        {
            this.data = default(T);
            this.lChild = ln;
            this.rChild = rn;
        }

        public Node(T data)
        {
            this.data = data;
            lChild = null;
            rChild = null;
        }

        public Node()
        {
            data = default(T);
            lChild = null;
            rChild = null;
        }

        public T Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public Node<T> LChild
        {
            get { return this.lChild; }
            set { this.lChild = value; }
        }

        public Node<T> RChild
        {
            get { return this.rChild; }
            set { this.rChild = value; }
        }
    }
    public class BiTree<T>
    {
        private Node<T> root;

        /// <summary>
        /// 根节点(属性)
        /// </summary>
        public Node<T> Root
        {
            get { return root; }
            set { root = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BiTree()
        {
            root = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public BiTree(T data)
        {
            Node<T> p = new Node<T>(data);
            root = p;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ln"></param>
        /// <param name="rn"></param>
        public BiTree(T data, Node<T> ln, Node<T> rn)
        {
            Node<T> p = new Node<T>(data, ln, rn);
            root = p;
        }

        /// <summary>
        /// 判断树是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (root == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取节点p的左子节点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Node<T> GetLChild(Node<T> p)
        {
            return p.LChild;
        }

        /// <summary>
        /// 获取节点p的右子节点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Node<T> GetRChild(Node<T> p)
        {
            return p.RChild;
        }

        /// <summary>
        /// 节点p下插入左子节点data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="p"></param>
        public void InsertL(T data, Node<T> p)
        {
            Node<T> tmp = new Node<T>(data);
            tmp.LChild = p.LChild;
            p.LChild = tmp;
        }

        /// <summary>
        /// 节点p下插入右子节点data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="p"></param>
        public void InsertR(T data, Node<T> p)
        {
            Node<T> tmp = new Node<T>(data);
            tmp.RChild = p.RChild;
            p.RChild = tmp;
        }

        /// <summary>
        /// 删除节点p的左子节点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Node<T> DeleteL(Node<T> p)
        {
            if ((p == null) || (p.LChild == null))
            {
                return null;
            }
            Node<T> tmp = p.LChild;
            p.LChild = null;
            return tmp;
        }

        /// <summary>
        /// 删除节点p的右子节点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Node<T> DeleteR(Node<T> p)
        {
            if ((p == null) || (p.RChild == null))
            {
                return null;
            }
            Node<T> tmp = p.RChild;
            p.RChild = null;

            return tmp;
        }

        /// <summary>
        /// 判断节点p是否为叶节点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsLeaf(Node<T> p)
        {
            if ((p != null) && (p.LChild == null) && (p.RChild == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
