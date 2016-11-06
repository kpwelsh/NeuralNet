using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNetModel
{
    class TrainingData
    {
        #region Properties
        public Vector<double> Data
        {
            get
            {
                return data?[0];
            }
            set
            {
                data[0] = value;
            }
        }
        public Vector<double> Response
        {
            get
            {
                return response?[0];
            }
            set
            {
                data[0] = value;
            }
        }
        public int Count
        {
            get
            {
                return data.Count;
            }
        }
        public TrainingPair this[int i]
        {
            get
            {
                return new TrainingPair(data[i], response[i]);
            }
            set
            {
                if (data.Count < i)
                    throw new NNException("Cannot set value out of index range.");
                else if (data.Count == i)
                    data.Add(value.Data);
                else if (data.Count > i)
                    data[i] = value.Data;

                if (response.Count < i)
                    throw new NNException("Cannot set value out of index range.");
                else if (response.Count == i)
                    response.Add(value.Response);
                else if (response.Count > i)
                    response[i] = value.Response;
            }
        }
        #endregion

        #region Private Fields
        private List<Vector<double>> data;
        private List<Vector<double>> response;
        #endregion

        #region Struct Definition
        public class TrainingPair
        {
            public Vector<double> Data = null;
            public Vector<double> Response = null;

            public TrainingPair()
            {

            }

            public TrainingPair(Vector<double> data, Vector<double> response)
            {
                Data = data;
                Response = response;
            }
        }
        #endregion

        #region Constructors
        public TrainingData(int dataLength, int reponseLength)
        {
            data = new List<Vector<double>>();
            response = new List<Vector<double>>();
            if (dataLength == 0)
                data.Add(null);
            else
                data.Add(new DenseVector(dataLength));
            response.Add(new DenseVector(reponseLength));
        }

        /// <summary>
        /// LOOK OUT! A PRIVATE CONSTRUCTOR!
        /// </summary>
        private TrainingData()
        {
            data = new List<Vector<double>>();
            response = new List<Vector<double>>();
        }
        #endregion

        public void AddTrainingPair(Vector<double> data, Vector<double> response)
        {
            data.Add(data);
            response.Add(response);
        }

        public TrainingData SubSequence(int start, int end)
        {
            int dir = Math.Sign(end - start);
            TrainingData td = new TrainingData();
            for(var i = start; i < end; i+= dir)
            {
                td.data.Add(data[i]);
                td.response.Add(response[i]);
            }
            return td;
        }

        public IEnumerator<TrainingPair> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return this[i];
        }
    }
}
