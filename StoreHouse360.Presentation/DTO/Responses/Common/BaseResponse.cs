﻿using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Presentation.DTO.Responses.Common
{
    public class BaseResponse<T>
    {
        [Required] 
        public ResponseMetaData MetaData { get; set; }
        public T? Data { get; set; }

        public BaseResponse(ResponseMetaData metaData, T? data)
        {
            MetaData = metaData;
            Data = data;
        }
    }
}
