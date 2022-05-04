using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class select : MonoBehaviour
{

    public Scrollbar bikes;

    public text bike;
    public text history;

    string currentbike;

    public GameObject classicbody;
    public GameObject cruiserbody;
    public GameObject racerbody;
    public bool mored;

    public Slider topspeed;
    public Slider acceleration;
    public Slider handling;
    public Slider boost;



    public void Start()
    {
        PlayerPrefs.SetString("bike", "classic");

        PlayerPrefs.Save();
        currentbike = PlayerPrefs.GetString("bike");

        bike.setText(currentbike);

        bikes.value = 0;
        history.setText("Classic Motorcycles are two wheeled motor vehicles that display elements of both bicycles and cars. The outward appearance strongly resembles a bicycle, with a metal frame and seat between two wheels, " +
            "the leading of which is controlled by a handlebar. However, rather than pedals, its propulsion relies on internal combustion, often throttled by the rotation of the right handle. The first 'true' motorcycle(i.e. internal combustion, petrol fuel) was created in 1885 " +
            "by Gottfried Daimler and William Maybach in Germany, and known as the Daimler Reitwagen. Throughout the late 19th century, motorcycles began to slowly enter the market,");


    classicbody.SetActive(true);
        cruiserbody.SetActive(false);
        racerbody.SetActive(false);

        topspeed.value = 3;
        acceleration.value = 3;
        handling.value = 3;
        boost.value = 3;





    }

    public void selectBike()
    {
        mored = false;
        if (bikes.value >= 0.75f)
        {
            currentbike = "racer";
            PlayerPrefs.SetString("bike", "racer");
            PlayerPrefs.Save();
            bike.setText(currentbike);
            history.setText("There are many classes of motorcycles created primarily for racing, major ones including MotoGP, Superbikes, and Supersport. The hallmark of racing motorcycles is an emphasis on optimized " +
                "performance to weight and much higher variation in techs and designs among bikes. MotoGP was introduced in the early 2000s, and featured varying purpose-built machines with maximum capacities of 800cc " +
                "and weight restrictions depending on cylinder numbers.");
            classicbody.SetActive(false);
            cruiserbody.SetActive(false);
            racerbody.SetActive(true);

            topspeed.value = 5;
            acceleration.value = 5;
            handling.value = 2;
            boost.value = 3;

        }
        else if (bikes.value > 0.25f)
        {
            currentbike = "cruiser";
            PlayerPrefs.SetString("bike", "cruiser");
            PlayerPrefs.Save();
            bike.setText(currentbike);
            history.setText("Cruiser Motorcycles are a subset of motorcycles for which style elements typical of machines from the 1930s to 1960s are incorporated. " +
                "The design requires the driver to ride with a position in which the feet are farther forward and the hands are up, with the back straight or slightly leaning backwards. " +
                "The seat is lower and the handlebars are wider. The engines usually emphasize easy rideability and gear shifts. It’s common amongst those who ride recreationally, but not competitively, cruisers have a more comfort oriented design.");
            classicbody.SetActive(false);
            cruiserbody.SetActive(true);
            racerbody.SetActive(false);

            topspeed.value = 3;
            acceleration.value = 2;
            handling.value = 5;
            boost.value = 5;

        }
        else
        {
            currentbike = "classic";
            PlayerPrefs.SetString("bike", "classic");
            PlayerPrefs.Save();
            bike.setText(currentbike);
            history.setText("Classic Motorcycles are two wheeled motor vehicles that display elements of both bicycles and cars. The outward appearance strongly resembles a bicycle, with a metal frame and seat between two wheels, " +
            "the leading of which is controlled by a handlebar. However, rather than pedals, its propulsion relies on internal combustion, often throttled by the rotation of the right handle. The first 'true' motorcycle(i.e. internal combustion, petrol fuel) was created in 1885 " +
            "by Gottfried Daimler and William Maybach in Germany, and known as the Daimler Reitwagen. Throughout the late 19th century, motorcycles began to slowly enter the market,");
            classicbody.SetActive(true);
            cruiserbody.SetActive(false);
            racerbody.SetActive(false);

            topspeed.value = 3;
            acceleration.value = 3;
            handling.value = 3;
            boost.value = 3;

        }
    }

    public void more()
    {
        if (mored == false)
        {
            if (bikes.value >= 0.75f)
            {
                history.setText("Superbike was introduced in the late 1980s and required motorcycles derived from homologated standard production models. They must have four stroke engines between 850cc and 1200cc for twin " +
                    "cylinders and 750cc and 1000cc for four cylinders. Supersport is a support category to Superbike, featuring similar homologation and four stroke requirements, however, engine capacities are 600cc to 750cc " +
                    "and 250cc to 600cc for twin and four cylinder engines, respectively.");
            }
            else if (bikes.value > 0.25f)
            {
                history.setText("The ground clearance is low and it is considerably less aerodynamic, sacrificing the speed of classical designs and postures for a better riding experience." +
                    " Japanese companies began to produce cruiser like models in the early 80’s, and, by 2000, they comprised over 60% of the motorcycle market, remaining a popular choice to this day.");
            }
            else
            {
                history.setText("with firms such as Indian and Harley-Davidson being established. However, what truly catalyzed the growth of the industry was the outbreak of WWI; " +
                    "motorcycles were an extremely effective method of supplying front line troops with information.The Model H, a 500cc, single cylinder bike considered the first “modern” motorcycle, " +
                    "was created by the Triumph Company, and over 30,000 were sold to the Allied Forces.Since then, steady innovation, such as the implementation of twin cylinders, and expansion have allowed motorcycles to become a multibillion dollar industry. ");
            }
            mored = true;
        }
        else
        {
            if (bikes.value >= 0.75f)
            {
                history.setText("There are many classes of motorcycles created primarily for racing, major ones including MotoGP, Superbikes, and Supersport. The hallmark of racing motorcycles is an emphasis on optimized " +
                "performance to weight and much higher variation in techs and designs among bikes. MotoGP was introduced in the early 2000s, and featured varying purpose-built machines with maximum capacities of 800cc " +
                "and weight restrictions depending on cylinder numbers.");
            }
            else if (bikes.value > 0.25f)
            {
                history.setText("Cruiser Motorcycles are a subset of motorcycles for which style elements typical of machines from the 1930s to 1960s are incorporated. " +
                "The design requires the driver to ride with a position in which the feet are farther forward and the hands are up, with the back straight or slightly leaning backwards. " +
                "The seat is lower and the handlebars are wider. The engines usually emphasize easy rideability and gear shifts. It’s common amongst those who ride recreationally, but not competitively, cruisers have a more comfort oriented design.");
            }
            else
            {
                history.setText("Classic Motorcycles are two wheeled motor vehicles that display elements of both bicycles and cars.The outward appearance strongly resembles a bicycle, with a metal frame and seat between two wheels, " +
            "the leading of which is controlled by a handlebar. However, rather than pedals, its propulsion relies on internal combustion, often throttled by the rotation of the right handle. The first 'true' motorcycle(i.e. internal combustion, petrol fuel) was created in 1885 " +
            "by Gottfried Daimler and William Maybach in Germany, and known as the Daimler Reitwagen. Throughout the late 19th century, motorcycles began to slowly enter the market,");
            }
            mored = false;
        }
    }


}
