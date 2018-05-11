$(document).ready(function()
{
    /* Initialize Tooltip */
    $('[data-toggle="tooltip"]').tooltip(); 
    
    /* Smooth scrolling í navbar og footer link */
    $(".navbar a, footer a[href='#myPage']").on('click', function(event)
    {
        if (this.hash !== "")
        {
        /* Koma í veg fyrir default anchor click behavior */
        event.preventDefault();
  
        /* Geyma hash */
        var hash = this.hash;
  
        /* Smooth page scroll
        900 segir til um scrolling hraða í millisekúndum */
        $('html, body').animate(
        {
          scrollTop: $(hash).offset().top
        }, 900, function()
            {
            /* Bætum Add hash (#) við URL þegar búið er að skrolla */
            window.location.hash = hash;
            });
        }
    });
})