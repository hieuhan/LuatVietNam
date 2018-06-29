/*Bengin Tab content*/
        function openCity(TabName, TabClass) {
            var i;
            var x = document.getElementsByClassName(TabClass);
            for (i = 0; i < x.length; i++) {
               x[i].style.display = "none";  
            }
            document.getElementById(TabName).style.display = "block";
        }
/*End Tab content*/