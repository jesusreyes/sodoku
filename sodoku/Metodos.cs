using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace sodoku
{
    public  class Metodos
    {
      //esta es una prueba
        public static int ct = 0;
        public static void CreaGrupos(ref Dictionary<int, Grupo> grupos)
        {
            grupos = BuildDictionary();
        }

        private static Dictionary<int, Grupo> BuildDictionary()
        {
            var grupos = new Dictionary<int, Grupo>();
            int x=0;
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    x++;
                    AddToDictionary(grupos, i, j, x, 0);
                }
            }
            return grupos;
        }

        private static void AddToDictionary(Dictionary<int, Grupo> grupos, int num, int pos, int key, int valor)
        {
            Grupo grupo = new Grupo();
            grupo.NoGrupo = num;
            grupo.Posicion = pos;
            grupo.Valor = valor;
            grupo.Renglon = ObtenRenglon(num, pos);
            grupo.Columna = ObtenColumna(num, pos);
            grupos.Add(key: key  , value: grupo);
        }

        public static void AgregaValor(ref Dictionary<int, Grupo> grupos, int pos, int gpo, int valor)
        {
            
            foreach (KeyValuePair<int, Grupo> kvp in grupos)//Busca en la colección de Grupos el elemento con el grupo y posición proporcionados
            {
                Grupo grupo = kvp.Value;

                if (grupo.NoGrupo == gpo && grupo.Posicion == pos)//Si encuentra el elemento Agrega Valor
                {
                    
                    grupo.Valor = valor;
                    //grupos.Remove(kvp.Key);
                    //grupos.Add(key: kvp.Key, value: grupo);
                    return;
                }
            }

        }

        public static void AgregaTodos(ref Dictionary<int, Grupo> grupos)
        {
            Random rnd = new Random();
            int cont=0;
            int gpo = 0;
            int pos = 0;
            int val = 0;
        
            do
            {
                pos = rnd.Next(1, 10);//Se toma un numero random entre 1 y 9 para la posición
                val = rnd.Next(1, 10);//Se toma un numero random entre 1 y 9 para el valor
                gpo = rnd.Next(1, 10);//Se toma un numero random entre 1 y 9 para el grupo

                if (PosicionDisponible(ref grupos, pos, gpo, val))//se verifica si la posición está disponible
                {
                    AgregaValor(ref grupos, pos, gpo, val);//Si esta disponible se agrega valor en la posición
                    cont++;
                }

            } while (cont < 81);//Se hace hasta que se llenen los 81 valores del Sudoku
        }

        private static bool PosicionDisponible(ref Dictionary<int, Grupo> grupos, int pos, int gpo, int valor)
        {
            foreach (KeyValuePair<int, Grupo> kvp in grupos)
            {
                //Busca en la colección el elemento con el grupo y posición proporcionados
                Grupo grupo = kvp.Value;
                // Si lo encuentra y el valor que trae es cero::Verifica Renglon y columna 
                if (grupo.NoGrupo==gpo && grupo.Posicion==pos && grupo.Valor == 0)
                {
                    if ((ValorEnColumna(ref grupos,pos,gpo,valor) || ( ValorEnRenglon(ref grupos,pos,gpo,valor))))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool ValorEnColumna(ref Dictionary<int, Grupo> grupos, int pos, int gpo, int valor) 
        {
           bool res=true;
           foreach (KeyValuePair<int, Grupo> kvp in grupos)
           {
               Grupo grupo = kvp.Value;
               // Si lo encuentra checa el valor que trae 
               if (grupo.Columna == ObtenColumna(gpo, pos))
               {
                   //Si es mayor que cero regresa que si hay valor en columna
                   if (grupo.Valor == valor)
                   {
                       return true;
                   }
                   else//sino pone como libre y sigue recorriendo
                   {
                       res = false;
                   }
               }
           }
           return res;//al final regresa el valor de la bandera
        }

        private static bool ValorEnRenglon(ref Dictionary<int, Grupo> grupos, int pos, int gpo, int valor)
        {
            bool res = true;
            foreach (KeyValuePair<int, Grupo> kvp in grupos)
            {
                Grupo grupo = kvp.Value;
                // Si lo encuentra checa el valor que trae 
                if (grupo.Renglon == ObtenRenglon(gpo, pos))
                {
                    if (grupo.Valor == valor)//Si es mayor que cero regresa que si hay valor en renglon
                    {
                        return true;
                    }
                    else//sino pone como libre y sigue recorriendo
                    {
                        res = false;
                    }
                }
            }
            return res;//al final regresa el valor de la bandera
        }

        //Obtiene el renglón por medio del grupo y la posición en el grupo
        private static int ObtenRenglon(int gpo, int pos)
        {
            int row = 0;
            
            switch (gpo)
            {
                case 1:
                case 2:
                case 3:   
                    switch (pos)
                    {
                        case 1:
                        case 2:
                        case 3:
                            row = 1;
                            return row;
                        case 4:
                        case 5:
                        case 6:
                            row = 2;
                            return row;
                        case 7:
                        case 8:
                        case 9:
                            row = 3;
                            return row;
                    }
                    return row;

                case 4:
                case 5:
                case 6:
                    switch (pos)
                    {
                        case 1:
                        case 2:
                        case 3:
                            row = 4;
                            return row;
                        case 4:
                        case 5:
                        case 6:
                            row = 5;
                            return row;
                        case 7:
                        case 8:
                        case 9:
                            row = 6;
                            return row;
                    }
                    return row;
                    
                case 7:
                case 8:
                case 9:
                    switch (pos)
                    {
                        case 1:
                        case 2:
                        case 3:
                            row = 7;
                            return row;
                        case 4:
                        case 5:
                        case 6:
                            row = 8;
                            return row;
                        case 7:
                        case 8:
                        case 9:
                            row = 9;
                            return row;
                    }
                    return row;
                default:
                    return row;
            }
        }

        //Obtiene la columna por medio del grupo y la posición en el grupo
        private static int ObtenColumna(int gpo, int pos) {
            int col = 0;

            switch (gpo)
            {
                case 1:
                case 4:
                case 7:
                    switch (pos)
                    {
                        case 1:
                        case 4:
                        case 7:
                            col = 1;
                            return col;
                        case 2:
                        case 5:
                        case 8:
                            col = 2;
                            return col;
                        case 3:
                        case 6:
                        case 9:
                            col = 3;
                            return col;
                    }
                    return col;

                case 2:
                case 5:
                case 8:
                    switch (pos)
                    {
                        case 1:
                        case 4:
                        case 7:
                            col = 4;
                            return col;
                        case 2:
                        case 5:
                        case 8:
                            col = 5;
                            return col;
                        case 3:
                        case 6:
                        case 9:
                            col = 6;
                            return col;
                    }
                    return col;

                case 3:
                case 6:
                case 9:
                    switch (pos)
                    {
                        case 1:
                        case 4:
                        case 7:
                            col = 7;
                            return col;
                        case 2:
                        case 5:
                        case 8:
                            col = 8;
                            return col;
                        case 3:
                        case 6:
                        case 9:
                            col = 9;
                            return col;
                    }
                    return col;
                default:
                    return col;
            }
        }
    }
}
