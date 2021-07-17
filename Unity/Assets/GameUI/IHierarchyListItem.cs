using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    /// <summary>
    /// �K�w�\�����X�g�̃A�C�e���C���^�[�t�F�[�X
    /// </summary>
    public interface IHierarchyListItem
    {
        /// <summary>
        /// �A�C�e���̎��ʎq
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���X�g�ŕ\�������ׂ�������
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// �e���X�g
        /// </summary>
        public IHierarchyListItem Parent { get; }

        /// <summary>
        /// �q�A�C�e�����X�g
        /// </summary>
        public List<IHierarchyListItem> Children { get; }
        
        /// <summary>
        /// �q�A�C�e����ǉ�����
        /// </summary>
        /// <param name="child">�q�A�C�e��</param>
        public void AddChild(IHierarchyListItem child);
    }
}