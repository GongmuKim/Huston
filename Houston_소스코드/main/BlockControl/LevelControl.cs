using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData
{
    public struct Range
    {
        public int min;
        public int max;
    };
    public float end_time;
    public float player_speed;

    public Range floor_count;
    public Range hole_count;
    public Range height_diff;

    public LevelData()
    {
        this.end_time = 15.0f;
        this.player_speed = 6.0f;
        this.floor_count.min = 10;
        this.floor_count.max = 10;
        this.hole_count.min = 2;
        this.height_diff.min = 0;
        this.height_diff.max = 0;

    }
}
public class LevelControl : MonoBehaviour
{
    private List<LevelData> level_datas = new List<LevelData>();

    public int HEIGHT_MAX = 20;
    public int HEIGHT_MIN = -4;
    public void loadLevelData(TextAsset level_data_text)
    {
        string level_texts = level_data_text.text;
        string[] lines = level_texts.Split('\n');

        foreach(var line in lines)
        {
            if(line == "")
            {
                continue;
            };
            Debug.Log(line);
            string[] words = line.Split();
            int n = 0;

            LevelData level_data = new LevelData();

            foreach (var word in words)
            {
                if (word.StartsWith("#"))
                {
                    break;
                }
                if (word == "")
                {
                    continue;
                }

                switch (n)
                {
                    case 0:
                        level_data.end_time = float.Parse(word);
                        break;
                    case 1:
                        level_data.player_speed = float.Parse(word);
                        break;
                    case 2:
                        level_data.floor_count.min = int.Parse(word);
                        break;
                    case 3:
                        level_data.floor_count.max = int.Parse(word);
                        break;
                    case 4:
                        level_data.hole_count.min = int.Parse(word);
                        break;
                    case 5:
                        level_data.hole_count.max = int.Parse(word);
                        break;
                    case 6:
                        level_data.height_diff.min = int.Parse(word);
                        break;
                    case 7:
                        level_data.height_diff.max = int.Parse(word);
                        break;

                }
                n++;
            }
            if (n >= 8)
            {
                this.level_datas.Add(level_data);
            }
            else
            {
                if (n == 0)
                {

                }
            }
        }
  }
    public struct CreationInfo
    {
        public Block.TYPE block_type;
        public int max_count;
        public int height;
        public int current_count;
    }
    public CreationInfo previous_block;
    public CreationInfo current_block;
    public CreationInfo next_block;

    public int block_count = 0;
    public int level = 0;

    private void clear_next_block(ref CreationInfo block)
    {
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 15;
        block.height = 0;
        block.current_count = 0;
    }
    public void initialize()
    {
        this.block_count = 0;

        this.clear_next_block(ref this.previous_block);
        this.clear_next_block(ref this.current_block);
        this.clear_next_block(ref this.next_block);
    }
    private void update_level(ref CreationInfo current, CreationInfo previous)
    {
        switch (previous.block_type)
        {
            case Block.TYPE.FLOOR:
                current.block_type = Block.TYPE.HOLE;
                current.max_count = 5;
                current.height = previous.height;
                break;
            case Block.TYPE.HOLE:
                current.block_type = Block.TYPE.FLOOR;
                current.max_count = 10;
                break;

        }
    }
    public void update()
    {
        this.current_block.current_count++;

        if (this.current_block.current_count >= this.current_block.max_count)
        {
            this.previous_block = this.current_block;
            this.current_block = this.next_block;

            this.clear_next_block(ref this.next_block);
            this.update_level(ref this.next_block, this.current_block);
        }
        this.block_count++;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
