/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright by The HDF Group.                                               *
 * Copyright by the Board of Trustees of the University of Illinois.         *
 * All rights reserved.                                                      *
 *                                                                           *
 * This file is part of HDF5.  The full HDF5 copyright notice, including     *
 * terms governing use, modification, and redistribution, is contained in    *
 * the files COPYING and Copyright.html.  COPYING can be found at the root   *
 * of the source code distribution tree; Copyright.html can be found at the  *
 * root level of an installed copy of the electronic HDF5 document set and   *
 * is linked from the top-level documents page.  It can also be found at     *
 * http://hdfgroup.org/HDF5/doc/Copyright.html.  If you do not have          *
 * access to either file, you may request a copy from help@hdfgroup.org.     *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


namespace HDF.PInvoke.HDF5;

using hid_t = System.Int64;

public sealed partial class H5P
{
    static readonly hid_t H5P_CLS_ROOT_g = H5DLLImporter.Instance.GetHid("H5P_CLS_ROOT_ID_g");

    public static hid_t ROOT { get { return H5P_CLS_ROOT_g; } }

    static readonly hid_t H5P_CLS_OBJECT_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_OBJECT_CREATE_ID_g");

    public static hid_t OBJECT_CREATE { get { return H5P_CLS_OBJECT_CREATE_g; } }

    static readonly hid_t H5P_CLS_FILE_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_FILE_CREATE_ID_g");

    public static hid_t FILE_CREATE { get { return H5P_CLS_FILE_CREATE_g; } }

    static readonly hid_t H5P_CLS_FILE_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_CLS_FILE_ACCESS_ID_g");

    public static hid_t FILE_ACCESS { get { return H5P_CLS_FILE_ACCESS_g; } }

    static readonly hid_t H5P_CLS_DATASET_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_DATASET_CREATE_ID_g");

    public static hid_t DATASET_CREATE { get { return H5P_CLS_DATASET_CREATE_g; } }

    static readonly hid_t H5P_CLS_DATASET_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_CLS_DATASET_ACCESS_ID_g");

    public static hid_t DATASET_ACCESS { get { return H5P_CLS_DATASET_ACCESS_g; } }

    static readonly hid_t H5P_CLS_DATASET_XFER_g = H5DLLImporter.Instance.GetHid("H5P_CLS_DATASET_XFER_ID_g");

    public static hid_t DATASET_XFER { get { return H5P_CLS_DATASET_XFER_g; } }

    static readonly hid_t H5P_CLS_FILE_MOUNT_g = H5DLLImporter.Instance.GetHid("H5P_CLS_FILE_MOUNT_ID_g");

    public static hid_t FILE_MOUNT { get { return H5P_CLS_FILE_MOUNT_g; } }

    static readonly hid_t H5P_CLS_GROUP_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_GROUP_CREATE_ID_g");

    public static hid_t GROUP_CREATE { get { return H5P_CLS_GROUP_CREATE_g; } }

    static readonly hid_t H5P_CLS_GROUP_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_CLS_GROUP_ACCESS_ID_g");

    public static hid_t GROUP_ACCESS { get { return H5P_CLS_GROUP_ACCESS_g; } }

    static readonly hid_t H5P_CLS_DATATYPE_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_DATATYPE_CREATE_ID_g");

    public static hid_t DATATYPE_CREATE { get { return H5P_CLS_DATATYPE_CREATE_g; } }

    static readonly hid_t H5P_CLS_DATATYPE_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_CLS_DATATYPE_ACCESS_ID_g");

    public static hid_t DATATYPE_ACCESS { get { return H5P_CLS_DATATYPE_ACCESS_g; } }

    static readonly hid_t H5P_CLS_STRING_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_STRING_CREATE_ID_g");

    public static hid_t STRING_CREATE { get { return H5P_CLS_STRING_CREATE_g; } }

    static readonly hid_t H5P_CLS_ATTRIBUTE_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_ATTRIBUTE_CREATE_ID_g");

    public static hid_t ATTRIBUTE_CREATE { get { return H5P_CLS_ATTRIBUTE_CREATE_g; } }

    static readonly hid_t H5P_CLS_OBJECT_COPY_g = H5DLLImporter.Instance.GetHid("H5P_CLS_OBJECT_COPY_ID_g");

    public static hid_t OBJECT_COPY { get { return H5P_CLS_OBJECT_COPY_g; } }

    static readonly hid_t H5P_CLS_LINK_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_CLS_LINK_CREATE_ID_g");

    public static hid_t LINK_CREATE { get { return H5P_CLS_LINK_CREATE_g; } }

    static readonly hid_t H5P_CLS_LINK_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_CLS_LINK_ACCESS_ID_g");

    public static hid_t LINK_ACCESS { get { return H5P_CLS_LINK_ACCESS_g; } }

    static readonly hid_t H5P_LST_FILE_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_LST_FILE_CREATE_ID_g");

    public static hid_t LST_FILE_CREATE { get { return H5P_LST_FILE_CREATE_g; } }

    static readonly hid_t H5P_LST_FILE_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_LST_FILE_ACCESS_ID_g");

    public static hid_t LST_FILE_ACCESS { get { return H5P_LST_FILE_ACCESS_g; } }

    static readonly hid_t H5P_LST_DATASET_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_LST_DATASET_CREATE_ID_g");

    public static hid_t LST_DATASET_CREATE { get { return H5P_LST_DATASET_CREATE_g; } }

    static readonly hid_t H5P_LST_DATASET_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_LST_DATASET_ACCESS_ID_g");

    public static hid_t LST_DATASET_ACCESS { get { return H5P_LST_DATASET_ACCESS_g; } }

    static readonly hid_t H5P_LST_DATASET_XFER_g = H5DLLImporter.Instance.GetHid("H5P_LST_DATASET_XFER_ID_g");

    public static hid_t LST_DATASET_XFER { get { return H5P_LST_DATASET_XFER_g; } }

    static readonly hid_t H5P_LST_FILE_MOUNT_g = H5DLLImporter.Instance.GetHid("H5P_LST_FILE_MOUNT_ID_g");

    public static hid_t LST_FILE_MOUNT { get { return H5P_LST_FILE_MOUNT_g; } }

    static readonly hid_t H5P_LST_GROUP_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_LST_GROUP_CREATE_ID_g");

    public static hid_t LST_GROUP_CREATE { get { return H5P_LST_GROUP_CREATE_g; } }

    static readonly hid_t H5P_LST_GROUP_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_LST_GROUP_ACCESS_ID_g");

    public static hid_t LST_GROUP_ACCESS { get { return H5P_LST_GROUP_ACCESS_g; } }

    static readonly hid_t H5P_LST_DATATYPE_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_LST_DATATYPE_CREATE_ID_g");

    public static hid_t LST_DATATYPE_CREATE { get { return H5P_LST_DATATYPE_CREATE_g; } }

    static readonly hid_t H5P_LST_DATATYPE_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_LST_DATATYPE_ACCESS_ID_g");

    public static hid_t LST_DATATYPE_ACCESS { get { return H5P_LST_DATATYPE_ACCESS_g; } }

    static readonly hid_t H5P_LST_ATTRIBUTE_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_LST_ATTRIBUTE_CREATE_ID_g");

    public static hid_t LST_ATTRIBUTE_CREATE { get { return H5P_LST_ATTRIBUTE_CREATE_g; } }

    static readonly hid_t H5P_LST_OBJECT_COPY_g = H5DLLImporter.Instance.GetHid("H5P_LST_OBJECT_COPY_ID_g");

    public static hid_t LST_OBJECT_COPY { get { return H5P_LST_OBJECT_COPY_g; } }

    static readonly hid_t H5P_LST_LINK_CREATE_g = H5DLLImporter.Instance.GetHid("H5P_LST_LINK_CREATE_ID_g");

    public static hid_t LST_LINK_CREATE { get { return H5P_LST_LINK_CREATE_g; } }

    static readonly hid_t H5P_LST_LINK_ACCESS_g = H5DLLImporter.Instance.GetHid("H5P_LST_LINK_ACCESS_ID_g");

    public static hid_t LST_LINK_ACCESS { get { return H5P_LST_LINK_ACCESS_g; } }
}
