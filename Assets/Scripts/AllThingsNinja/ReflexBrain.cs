using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexBrain : Brain
{
    GameObject Pirate;

    /// <summary>
    /// gets the next move for the ninja to make
    /// </summary>
    /// <returns></returns>
    public override Vector2 getNext()
    {
        float speed_factor = NinjaConfiguration.MovementSpeed;
        legal.Clear();
        legal = get_legal_move(speed_factor);
        List<Vector2> best_score = new List<Vector2>();
        float hi_score = 0;

        Pirate = GameObject.Find("pirate_idle_0");

        foreach(KeyValuePair<LegalMoves, Vector2> pair in legal)
        {
            float temp = getScore(pair.Value);
            if(temp == hi_score)
            {
                best_score.Add(pair.Value);
            }
            else if(temp > hi_score)
            {
                best_score.Clear();
                best_score.Add(pair.Value);
                hi_score = temp;
            }
        }
        int choice = Random.Range(0, best_score.Count);
        newPosition = best_score[choice];
        return newPosition;
    }

    /// <summary>
    /// provides scoring for each legal movement the ninja can make
    /// </summary>
    /// <param name="potential"></param>
    /// <returns></returns>
    private float getScore(Vector2 potential)
    {
        float score = 0;
        // check distance from pirate
        score += close_distance(potential);
        score += getLOS(potential);
        if (Configuration.Difficulty == DifficultyLevels.Hard)
        {
            score += avoidDrunk(potential);
        }
        return score;
    }

    /// <summary>
    /// increases score for moves that get closer to the Pirate, 0 points awarded other wise
    /// </summary>
    /// <param name="potential"></param>
    /// <returns></returns>
    private float close_distance(Vector2 potential)
    {
        if (Pirate != null)
        {
            float distance = Vector2.Distance(potential, Pirate.GetComponent<Rigidbody2D>().position);
            if (Vector2.Distance(transform.position, Pirate.GetComponent<Rigidbody2D>().position) > distance)
            {
                return 10f;
            }
            else
            {
                return 0f;
            }
        }
        else
        {
            return 0f;
        }
        
    }
    
    /// <summary>
    /// increases score heavily if the movement will put the pirate in Line of sight
    /// </summary>
    /// <param name="potential"></param>
    /// <returns></returns>
    private float getLOS(Vector2 potential)
    {
        Vector2 goal;
        Vector2 direction;
        
        if (Pirate != null)
        {
            goal = Pirate.GetComponent<Rigidbody2D>().position;
            direction = goal - potential;
            //List<RaycastHit2D> results = new List<RaycastHit2D>();

            // Line of sight to pirate
            RaycastHit2D sighttest = Physics2D.Raycast(potential, direction, 10f);

            Debug.DrawRay(transform.position, goal);

            if (sighttest && sighttest.collider.CompareTag("ARR"))
            {
                return 100f;
            }
            else return 0f;
        }        
        else
        {
            return 0f;
        }
    }
    
    float avoidDrunk(Vector2 potential)
    {
        if (Pirate != null)
        {
            bool intoxication = Pirate.GetComponent<Pirate>().Drunk;
            if (intoxication == true)
            {
                float distance = Vector2.Distance(potential, Pirate.GetComponent<Rigidbody2D>().position);
                if (Vector2.Distance(transform.position, Pirate.GetComponent<Rigidbody2D>().position) < distance)
                {
                    return 110f;
                }
                else
                {
                    return 0f;
                }
            }
            else
            {
                return 0f;
            }
            
        }
        else
        {
            return 0f;
        }

    }


}
