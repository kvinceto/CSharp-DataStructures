namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> membersById;
        private Dictionary<int, Trainer> trainersById;

        public FitGym()
        {
            membersById = new Dictionary<int, Member>();
            trainersById = new Dictionary<int, Trainer>();
        }

        public void AddMember(Member member)
        {
            if (membersById.ContainsKey(member.Id))
            {
                throw new ArgumentException();
            }

            membersById.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            trainersById.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!membersById.ContainsKey(member.Id))
            {
                membersById.Add(member.Id, member);
            }

            if (!trainersById.ContainsKey(trainer.Id) || member.Trainer != null)
            {
                throw new ArgumentException();
            }

            member.Trainer = trainer;
            trainer.Members[member.Id] = member;
        }

        public bool Contains(Member member)
        {
            return membersById.ContainsKey(member.Id);
        }

        public bool Contains(Trainer trainer)
        {
            return trainersById.ContainsKey(trainer.Id);
        }

        public Trainer FireTrainer(int id)
        {
            if (!trainersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }
            Trainer trainer = trainersById[id];

            foreach (var member in trainer.Members.Values)
            {
                member.Trainer = null;
            }

            trainersById.Remove(id);
            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!membersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            Member member = membersById[id];
            membersById.Remove(id);
            if (member.Trainer != null)
            {
                member.Trainer.Members.Remove(id);
            }
           
            return member;
        }

        public int MemberCount => membersById.Count;

        public int TrainerCount => trainersById.Count;

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return membersById.Values
                .OrderBy(m => m.RegistrationDate)
                .ThenByDescending(m => m.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return trainersById.Values
                .OrderBy(t => t.Popularity);
        }

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            return trainersById[trainer.Id].Members.Values
                .OrderBy(m => m.RegistrationDate)
                .ThenBy(m => m.Name);
        }

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {
            return membersById.Values
                .Where(m => m.Trainer.Popularity >= lo && m.Trainer.Popularity <= hi)
                .OrderBy(m => m.Visits)
                .ThenBy(m => m.Name);
        }

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            Dictionary<Trainer, HashSet<Member>> result = new Dictionary<Trainer, HashSet<Member>>();

            foreach (var trainer in trainersById.Values)
            {
                HashSet<Member> members = new HashSet<Member>();
                foreach (var member in trainer.Members.Values)
                {
                    members.Add(member);
                }

                result.Add(trainer, members);
            }

            return result;
        }
    }
}