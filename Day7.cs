using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_2018
{
    class Day7Instruction
    {
        public string name;
        public List<Day7Instruction> prerequisites = new List<Day7Instruction>();
        public bool completed = false;

        public int time;

        public Day7Instruction(string name)
        {
            this.name=name;
            time=60+(name[0]-'A'+1);
            //Console.WriteLine($"{name} time: {time}");
        }

        public override string ToString()
        {
            return name; 
        }
    }
    class Day7Worker
    {
        public Day7Instruction assignedInstruction = null;
        public bool Work()
        {
            if (assignedInstruction == null)
                return false;

            assignedInstruction.time--;
            if (assignedInstruction.time==0)
            {
                assignedInstruction.completed=true;
                //assignedInstruction=null;
                return true;
            }
            return false;
        }
    }

    class Day7 : IDay
    {
        public void Do()
        {
            StreamReader sr = new StreamReader("inputs/day7.txt");
            List<Day7Instruction> instructions = new List<Day7Instruction>();

            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                MatchCollection m = Regex.Matches(line, @"[A-Z]");
                Day7Instruction ins = instructions.Where(x=>x.name == m[2].Value).FirstOrDefault();
                if (ins == null)
                {
                    ins = new Day7Instruction(m[2].Value);
                    instructions.Add(ins);
                }                
                Day7Instruction pre = instructions.Where(x=>x.name==m[1].Value).FirstOrDefault();
                if (pre == null)
                {
                    pre = new Day7Instruction(m[1].Value);
                    instructions.Add(pre);
                }
                ins.prerequisites.Add(pre);
            }
            StringBuilder path = new StringBuilder();
            bool remaining=true;
            instructions = instructions.OrderBy(x=>x.name).ToList();

            while (remaining)
            {
                remaining=false;
                foreach (Day7Instruction ins in instructions.ToArray())
                {
                    if (ins.completed)
                        continue;

                    if (ins.prerequisites.Count == 0)
                        ins.completed=true;
                    else
                    {
                        bool x=true;
                        foreach (Day7Instruction a  in ins.prerequisites)
                        {
                            if (!a.completed)
                                x=false;
                        }
                        ins.completed = x;
                    }
                    if (!ins.completed)
                        remaining=true;
                    else
                    {
                        path.Append(ins.name);
                        remaining=true;
                        break;
                    }
                }
            }
            Console.WriteLine("Part 1: " + path);
            foreach (Day7Instruction ins in instructions)
                ins.completed=false;
            
            path.Clear();
            remaining=true;
            int elapsedTime=-1;
            List<Day7Worker> workers = new List<Day7Worker>();
            for (int i=0; i < 5; i++)
                workers.Add(new Day7Worker());

            //Console.WriteLine(workers.Count);
            instructions = instructions.OrderBy(x=>x.name).ToList();

            while (remaining)
            {
                elapsedTime++;
                remaining=false;
                for (int i=0; i < workers.Count; i++)
                {
                    if (workers[i].assignedInstruction == null)
                    {
                        Day7Instruction assigned = null;
                        foreach (Day7Instruction ins in instructions.ToArray())
                        {

                            if (ins.prerequisites.Count == 0)
                            {
                                assigned=ins;
                                break;
                            }
                            else
                            {
                                bool x=true;
                                foreach (Day7Instruction a  in ins.prerequisites)
                                {
                                    if (!a.completed)
                                        x=false;
                                }
                                if (x)
                                {
                                    assigned=ins;
                                    break;
                                }
                            }
                        }
                        if (assigned != null)
                        {
                            instructions.Remove(assigned);
                            instructions = instructions.OrderBy(x=>x.name).ToList();
                            workers[i].assignedInstruction=assigned;
                        }
                    }
                }
                for (int i=0;i<workers.Count;i++)
                {
                    //Console.WriteLine(elapsedTime +": Worker " + i + " working on " + (workers[i].assignedInstruction != null ? workers[i].assignedInstruction.name : "nothing"));
                    if (workers[i].assignedInstruction != null)
                    {
                        if (workers[i].Work())
                        {
                            path.Append(workers[i].assignedInstruction.name);
                            workers[i].assignedInstruction=null;
                        }
                        remaining=true;
                    }
                }
                //Console.WriteLine(elapsedTime + ": done - " + path);
            }
            Console.WriteLine($"Part 2: {elapsedTime} {path}");
        }
    }
}