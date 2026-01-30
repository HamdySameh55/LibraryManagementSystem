using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Services
{
    public class MemberService : ISearchable<Member>, IPrintable
    {
        private readonly List<Member> members;

        public MemberService(List<Member> members)
        {
            this.members = members; // رابط مباشر مع DataManager.Members
        }

        // ================= Search =================
        public Member SearchById(int id)
        {
            return members.FirstOrDefault(m => m.Id == id);
        }

        public List<Member> SearchByName(string name)
        {
            return members
                .Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Member> GetAll()
        {
            return members;
        }

        public List<Member> GetActiveMembers()
        {
            return members.Where(m => m.IsActive).ToList();
        }

        // ================= CRUD =================
        public void RegisterMember(Member member)
        {
            if (member == null) return;
            if (members.Any(m => m.Id == member.Id)) return;
            member.IsActive = true;
            member.CurrentBorrowedBooks = 0;
            members.Add(member);
        }

        public void RenewMembership(int memberId)
        {
            var member = members.FirstOrDefault(m => m.Id == memberId);
            if (member != null)
            {
                member.IsActive = true;
            }
        }

        public void RemoveMember(int memberId)
        {
            var member = members.FirstOrDefault(m => m.Id == memberId);
            if (member != null) members.Remove(member);
        }

        // ================= Print =================
        public void PrintReceipt()
        {
            Console.WriteLine("=== Members Receipt ===");
            foreach (var m in members)
                Console.WriteLine($"ID: {m.Id}, Name: {m.Name}, Email: {m.Email}");
            Console.WriteLine("====================\n");
        }

        public void PrintReport()
        {
            Console.WriteLine("=== Members Report ===");
            foreach (var m in members)
                Console.WriteLine($"ID: {m.Id}, Name: {m.Name}, Email: {m.Email}, Active: {m.IsActive}");
            Console.WriteLine("===================\n");
        }

        public void PrintDetails()
        {
            foreach (var m in members)
                Console.WriteLine($"{m.Id} - {m.Name} - {m.Email} - Borrowed: {m.CurrentBorrowedBooks}");
        }
    }
}
