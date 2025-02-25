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

public sealed partial class H5E
{
    static readonly hid_t H5E_EARRAY_g = H5DLLImporter.Instance.GetHid("H5E_EARRAY_g");

    public static hid_t EARRAY { get { return H5E_EARRAY_g; } }

    static readonly hid_t H5E_FARRAY_g = H5DLLImporter.Instance.GetHid("H5E_FARRAY_g");

    public static hid_t FARRAY { get { return H5E_FARRAY_g; } }

    static readonly hid_t H5E_CANTDEPEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTDEPEND_g");

    public static hid_t CANTDEPEND { get { return H5E_CANTDEPEND_g; } }

    static readonly hid_t H5E_CANTUNDEPEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNDEPEND_g");

    public static hid_t CANTUNDEPEND { get { return H5E_CANTUNDEPEND_g; } }

    static readonly hid_t H5E_CANTNOTIFY_g = H5DLLImporter.Instance.GetHid("H5E_CANTNOTIFY_g");

    public static hid_t CANTNOTIFY { get { return H5E_CANTNOTIFY_g; } }

    static readonly hid_t H5E_LOGFAIL_g = H5DLLImporter.Instance.GetHid("H5E_LOGFAIL_g");

    public static hid_t LOGFAIL { get { return H5E_LOGFAIL_g; } }

    static readonly hid_t H5E_CANTCORK_g = H5DLLImporter.Instance.GetHid("H5E_CANTCORK_g");

    public static hid_t CANTCORK { get { return H5E_CANTCORK_g; } }

    static readonly hid_t H5E_CANTUNCORK_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNCORK_g");

    public static hid_t CANTUNCORK { get { return H5E_CANTUNCORK_g; } }

    static readonly hid_t H5E_CANTAPPEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTAPPEND_g");

    public static hid_t CANTAPPEND { get { return H5E_CANTAPPEND_g; } }

    static readonly hid_t H5E_ERR_CLS_g = H5DLLImporter.Instance.GetHid("H5E_ERR_CLS_g");

    public static hid_t ERR_CLS { get { return H5E_ERR_CLS_g; } }

    static readonly hid_t H5E_DATASET_g = H5DLLImporter.Instance.GetHid("H5E_DATASET_g");

    public static hid_t DATASET { get { return H5E_DATASET_g; } }

    static readonly hid_t H5E_FUNC_g = H5DLLImporter.Instance.GetHid("H5E_FUNC_g");

    public static hid_t FUNC { get { return H5E_FUNC_g; } }

    static readonly hid_t H5E_STORAGE_g = H5DLLImporter.Instance.GetHid("H5E_STORAGE_g");

    public static hid_t STORAGE { get { return H5E_STORAGE_g; } }

    static readonly hid_t H5E_FILE_g = H5DLLImporter.Instance.GetHid("H5E_FILE_g");

    public static hid_t FILE { get { return H5E_FILE_g; } }

    static readonly hid_t H5E_SOHM_g = H5DLLImporter.Instance.GetHid("H5E_SOHM_g");

    public static hid_t SOHM { get { return H5E_SOHM_g; } }

    static readonly hid_t H5E_SYM_g = H5DLLImporter.Instance.GetHid("H5E_SYM_g");

    public static hid_t SYM { get { return H5E_SYM_g; } }

    static readonly hid_t H5E_PLUGIN_g = H5DLLImporter.Instance.GetHid("H5E_PLUGIN_g");

    public static hid_t PLUGIN { get { return H5E_PLUGIN_g; } }

    static readonly hid_t H5E_VFL_g = H5DLLImporter.Instance.GetHid("H5E_VFL_g");

    public static hid_t VFL { get { return H5E_VFL_g; } }

    static readonly hid_t H5E_INTERNAL_g = H5DLLImporter.Instance.GetHid("H5E_INTERNAL_g");

    public static hid_t INTERNAL { get { return H5E_INTERNAL_g; } }

    static readonly hid_t H5E_BTREE_g = H5DLLImporter.Instance.GetHid("H5E_BTREE_g");

    public static hid_t BTREE { get { return H5E_BTREE_g; } }

    static readonly hid_t H5E_REFERENCE_g = H5DLLImporter.Instance.GetHid("H5E_REFERENCE_g");

    public static hid_t REFERENCE { get { return H5E_REFERENCE_g; } }

    static readonly hid_t H5E_DATASPACE_g = H5DLLImporter.Instance.GetHid("H5E_DATASPACE_g");

    public static hid_t DATASPACE { get { return H5E_DATASPACE_g; } }

    static readonly hid_t H5E_RESOURCE_g = H5DLLImporter.Instance.GetHid("H5E_RESOURCE_g");

    public static hid_t RESOURCE { get { return H5E_RESOURCE_g; } }

    static readonly hid_t H5E_PLIST_g = H5DLLImporter.Instance.GetHid("H5E_PLIST_g");

    public static hid_t PLIST { get { return H5E_PLIST_g; } }

    static readonly hid_t H5E_LINK_g = H5DLLImporter.Instance.GetHid("H5E_LINK_g");

    public static hid_t LINK { get { return H5E_LINK_g; } }

    static readonly hid_t H5E_DATATYPE_g = H5DLLImporter.Instance.GetHid("H5E_DATATYPE_g");

    public static hid_t DATATYPE { get { return H5E_DATATYPE_g; } }

    static readonly hid_t H5E_RS_g = H5DLLImporter.Instance.GetHid("H5E_RS_g");

    public static hid_t RS { get { return H5E_RS_g; } }

    static readonly hid_t H5E_HEAP_g = H5DLLImporter.Instance.GetHid("H5E_HEAP_g");

    public static hid_t HEAP { get { return H5E_HEAP_g; } }

    static readonly hid_t H5E_OHDR_g = H5DLLImporter.Instance.GetHid("H5E_OHDR_g");

    public static hid_t OHDR { get { return H5E_OHDR_g; } }

    static readonly hid_t H5E_ATOM_g = H5DLLImporter.Instance.GetHid("H5E_ATOM_g");

    public static hid_t ATOM { get { return H5E_ATOM_g; } }

    static readonly hid_t H5E_ATTR_g = H5DLLImporter.Instance.GetHid("H5E_ATTR_g");

    public static hid_t ATTR { get { return H5E_ATTR_g; } }

    static readonly hid_t H5E_NONE_MAJOR_g = H5DLLImporter.Instance.GetHid("H5E_NONE_MAJOR_g");

    public static hid_t NONE_MAJOR { get { return H5E_NONE_MAJOR_g; } }

    static readonly hid_t H5E_IO_g = H5DLLImporter.Instance.GetHid("H5E_IO_g");

    public static hid_t IO { get { return H5E_IO_g; } }

    static readonly hid_t H5E_SLIST_g = H5DLLImporter.Instance.GetHid("H5E_SLIST_g");

    public static hid_t SLIST { get { return H5E_SLIST_g; } }

    static readonly hid_t H5E_EFL_g = H5DLLImporter.Instance.GetHid("H5E_EFL_g");

    public static hid_t EFL { get { return H5E_EFL_g; } }

    static readonly hid_t H5E_TST_g = H5DLLImporter.Instance.GetHid("H5E_TST_g");

    public static hid_t TST { get { return H5E_TST_g; } }

    static readonly hid_t H5E_ARGS_g = H5DLLImporter.Instance.GetHid("H5E_ARGS_g");

    public static hid_t ARGS { get { return H5E_ARGS_g; } }

    static readonly hid_t H5E_ERROR_g = H5DLLImporter.Instance.GetHid("H5E_ERROR_g");

    public static hid_t ERROR { get { return H5E_ERROR_g; } }

    static readonly hid_t H5E_PLINE_g = H5DLLImporter.Instance.GetHid("H5E_PLINE_g");

    public static hid_t PLINE { get { return H5E_PLINE_g; } }

    static readonly hid_t H5E_FSPACE_g = H5DLLImporter.Instance.GetHid("H5E_FSPACE_g");

    public static hid_t FSPACE { get { return H5E_FSPACE_g; } }

    static readonly hid_t H5E_CACHE_g = H5DLLImporter.Instance.GetHid("H5E_CACHE_g");

    public static hid_t CACHE { get { return H5E_CACHE_g; } }

    static readonly hid_t H5E_SEEKERROR_g = H5DLLImporter.Instance.GetHid("H5E_SEEKERROR_g");

    public static hid_t SEEKERROR { get { return H5E_SEEKERROR_g; } }

    static readonly hid_t H5E_READERROR_g = H5DLLImporter.Instance.GetHid("H5E_READERROR_g");

    public static hid_t READERROR { get { return H5E_READERROR_g; } }

    static readonly hid_t H5E_WRITEERROR_g = H5DLLImporter.Instance.GetHid("H5E_WRITEERROR_g");

    public static hid_t WRITEERROR { get { return H5E_WRITEERROR_g; } }

    static readonly hid_t H5E_CLOSEERROR_g = H5DLLImporter.Instance.GetHid("H5E_CLOSEERROR_g");

    public static hid_t CLOSEERROR { get { return H5E_CLOSEERROR_g; } }

    static readonly hid_t H5E_OVERFLOW_g = H5DLLImporter.Instance.GetHid("H5E_OVERFLOW_g");

    public static hid_t OVERFLOW { get { return H5E_OVERFLOW_g; } }

    static readonly hid_t H5E_FCNTL_g = H5DLLImporter.Instance.GetHid("H5E_FCNTL_g");

    public static hid_t FCNTL { get { return H5E_FCNTL_g; } }

    static readonly hid_t H5E_NOSPACE_g = H5DLLImporter.Instance.GetHid("H5E_NOSPACE_g");

    public static hid_t NOSPACE { get { return H5E_NOSPACE_g; } }

    static readonly hid_t H5E_CANTALLOC_g = H5DLLImporter.Instance.GetHid("H5E_CANTALLOC_g");

    public static hid_t CANTALLOC { get { return H5E_CANTALLOC_g; } }

    static readonly hid_t H5E_CANTCOPY_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOPY_g");

    public static hid_t CANTCOPY { get { return H5E_CANTCOPY_g; } }

    static readonly hid_t H5E_CANTFREE_g = H5DLLImporter.Instance.GetHid("H5E_CANTFREE_g");

    public static hid_t CANTFREE { get { return H5E_CANTFREE_g; } }

    static readonly hid_t H5E_ALREADYEXISTS_g = H5DLLImporter.Instance.GetHid("H5E_ALREADYEXISTS_g");

    public static hid_t ALREADYEXISTS { get { return H5E_ALREADYEXISTS_g; } }

    static readonly hid_t H5E_CANTLOCK_g = H5DLLImporter.Instance.GetHid("H5E_CANTLOCK_g");

    public static hid_t CANTLOCK { get { return H5E_CANTLOCK_g; } }

    static readonly hid_t H5E_CANTUNLOCK_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNLOCK_g");

    public static hid_t CANTUNLOCK { get { return H5E_CANTUNLOCK_g; } }

    static readonly hid_t H5E_CANTGC_g = H5DLLImporter.Instance.GetHid("H5E_CANTGC_g");

    public static hid_t CANTGC { get { return H5E_CANTGC_g; } }

    static readonly hid_t H5E_CANTGETSIZE_g = H5DLLImporter.Instance.GetHid("H5E_CANTGETSIZE_g");

    public static hid_t CANTGETSIZE { get { return H5E_CANTGETSIZE_g; } }

    static readonly hid_t H5E_OBJOPEN_g = H5DLLImporter.Instance.GetHid("H5E_OBJOPEN_g");

    public static hid_t OBJOPEN { get { return H5E_OBJOPEN_g; } }

    static readonly hid_t H5E_CANTRESTORE_g = H5DLLImporter.Instance.GetHid("H5E_CANTRESTORE_g");

    public static hid_t CANTRESTORE { get { return H5E_CANTRESTORE_g; } }

    static readonly hid_t H5E_CANTCOMPUTE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOMPUTE_g");

    public static hid_t CANTCOMPUTE { get { return H5E_CANTCOMPUTE_g; } }

    static readonly hid_t H5E_CANTEXTEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTEXTEND_g");

    public static hid_t CANTEXTEND { get { return H5E_CANTEXTEND_g; } }

    static readonly hid_t H5E_CANTATTACH_g = H5DLLImporter.Instance.GetHid("H5E_CANTATTACH_g");

    public static hid_t CANTATTACH { get { return H5E_CANTATTACH_g; } }

    static readonly hid_t H5E_CANTUPDATE_g = H5DLLImporter.Instance.GetHid("H5E_CANTUPDATE_g");

    public static hid_t CANTUPDATE { get { return H5E_CANTUPDATE_g; } }

    static readonly hid_t H5E_CANTOPERATE_g = H5DLLImporter.Instance.GetHid("H5E_CANTOPERATE_g");

    public static hid_t CANTOPERATE { get { return H5E_CANTOPERATE_g; } }

    static readonly hid_t H5E_CANTINIT_g = H5DLLImporter.Instance.GetHid("H5E_CANTINIT_g");

    public static hid_t CANTINIT { get { return H5E_CANTINIT_g; } }

    static readonly hid_t H5E_ALREADYINIT_g = H5DLLImporter.Instance.GetHid("H5E_ALREADYINIT_g");

    public static hid_t ALREADYINIT { get { return H5E_ALREADYINIT_g; } }

    static readonly hid_t H5E_CANTRELEASE_g = H5DLLImporter.Instance.GetHid("H5E_CANTRELEASE_g");

    public static hid_t CANTRELEASE { get { return H5E_CANTRELEASE_g; } }

    static readonly hid_t H5E_CANTGET_g = H5DLLImporter.Instance.GetHid("H5E_CANTGET_g");

    public static hid_t CANTGET { get { return H5E_CANTGET_g; } }

    static readonly hid_t H5E_CANTSET_g = H5DLLImporter.Instance.GetHid("H5E_CANTSET_g");

    public static hid_t CANTSET { get { return H5E_CANTSET_g; } }

    static readonly hid_t H5E_DUPCLASS_g = H5DLLImporter.Instance.GetHid("H5E_DUPCLASS_g");

    public static hid_t DUPCLASS { get { return H5E_DUPCLASS_g; } }

    static readonly hid_t H5E_SETDISALLOWED_g = H5DLLImporter.Instance.GetHid("H5E_SETDISALLOWED_g");

    public static hid_t SETDISALLOWED { get { return H5E_SETDISALLOWED_g; } }

    static readonly hid_t H5E_CANTMERGE_g = H5DLLImporter.Instance.GetHid("H5E_CANTMERGE_g");

    public static hid_t CANTMERGE { get { return H5E_CANTMERGE_g; } }

    static readonly hid_t H5E_CANTREVIVE_g = H5DLLImporter.Instance.GetHid("H5E_CANTREVIVE_g");

    public static hid_t CANTREVIVE { get { return H5E_CANTREVIVE_g; } }

    static readonly hid_t H5E_CANTSHRINK_g = H5DLLImporter.Instance.GetHid("H5E_CANTSHRINK_g");

    public static hid_t CANTSHRINK { get { return H5E_CANTSHRINK_g; } }

    static readonly hid_t H5E_LINKCOUNT_g = H5DLLImporter.Instance.GetHid("H5E_LINKCOUNT_g");

    public static hid_t LINKCOUNT { get { return H5E_LINKCOUNT_g; } }

    static readonly hid_t H5E_VERSION_g = H5DLLImporter.Instance.GetHid("H5E_VERSION_g");

    public static hid_t VERSION { get { return H5E_VERSION_g; } }

    static readonly hid_t H5E_ALIGNMENT_g = H5DLLImporter.Instance.GetHid("H5E_ALIGNMENT_g");

    public static hid_t ALIGNMENT { get { return H5E_ALIGNMENT_g; } }

    static readonly hid_t H5E_BADMESG_g = H5DLLImporter.Instance.GetHid("H5E_BADMESG_g");

    public static hid_t BADMESG { get { return H5E_BADMESG_g; } }

    static readonly hid_t H5E_CANTDELETE_g = H5DLLImporter.Instance.GetHid("H5E_CANTDELETE_g");

    public static hid_t CANTDELETE { get { return H5E_CANTDELETE_g; } }

    static readonly hid_t H5E_BADITER_g = H5DLLImporter.Instance.GetHid("H5E_BADITER_g");

    public static hid_t BADITER { get { return H5E_BADITER_g; } }

    static readonly hid_t H5E_CANTPACK_g = H5DLLImporter.Instance.GetHid("H5E_CANTPACK_g");

    public static hid_t CANTPACK { get { return H5E_CANTPACK_g; } }

    static readonly hid_t H5E_CANTRESET_g = H5DLLImporter.Instance.GetHid("H5E_CANTRESET_g");

    public static hid_t CANTRESET { get { return H5E_CANTRESET_g; } }

    static readonly hid_t H5E_CANTRENAME_g = H5DLLImporter.Instance.GetHid("H5E_CANTRENAME_g");

    public static hid_t CANTRENAME { get { return H5E_CANTRENAME_g; } }

    static readonly hid_t H5E_SYSERRSTR_g = H5DLLImporter.Instance.GetHid("H5E_SYSERRSTR_g");

    public static hid_t SYSERRSTR { get { return H5E_SYSERRSTR_g; } }

    static readonly hid_t H5E_NOFILTER_g = H5DLLImporter.Instance.GetHid("H5E_NOFILTER_g");

    public static hid_t NOFILTER { get { return H5E_NOFILTER_g; } }

    static readonly hid_t H5E_CALLBACK_g = H5DLLImporter.Instance.GetHid("H5E_CALLBACK_g");

    public static hid_t CALLBACK { get { return H5E_CALLBACK_g; } }

    static readonly hid_t H5E_CANAPPLY_g = H5DLLImporter.Instance.GetHid("H5E_CANAPPLY_g");

    public static hid_t CANAPPLY { get { return H5E_CANAPPLY_g; } }

    static readonly hid_t H5E_SETLOCAL_g = H5DLLImporter.Instance.GetHid("H5E_SETLOCAL_g");

    public static hid_t SETLOCAL { get { return H5E_SETLOCAL_g; } }

    static readonly hid_t H5E_NOENCODER_g = H5DLLImporter.Instance.GetHid("H5E_NOENCODER_g");

    public static hid_t NOENCODER { get { return H5E_NOENCODER_g; } }

    static readonly hid_t H5E_CANTFILTER_g = H5DLLImporter.Instance.GetHid("H5E_CANTFILTER_g");

    public static hid_t CANTFILTER { get { return H5E_CANTFILTER_g; } }

    static readonly hid_t H5E_CANTOPENOBJ_g = H5DLLImporter.Instance.GetHid("H5E_CANTOPENOBJ_g");

    public static hid_t CANTOPENOBJ { get { return H5E_CANTOPENOBJ_g; } }

    static readonly hid_t H5E_CANTCLOSEOBJ_g = H5DLLImporter.Instance.GetHid("H5E_CANTCLOSEOBJ_g");

    public static hid_t CANTCLOSEOBJ { get { return H5E_CANTCLOSEOBJ_g; } }

    static readonly hid_t H5E_COMPLEN_g = H5DLLImporter.Instance.GetHid("H5E_COMPLEN_g");

    public static hid_t COMPLEN { get { return H5E_COMPLEN_g; } }

    static readonly hid_t H5E_PATH_g = H5DLLImporter.Instance.GetHid("H5E_PATH_g");

    public static hid_t PATH { get { return H5E_PATH_g; } }

    static readonly hid_t H5E_NONE_MINOR_g = H5DLLImporter.Instance.GetHid("H5E_NONE_MINOR_g");

    public static hid_t NONE_MINOR { get { return H5E_NONE_MINOR_g; } }

    static readonly hid_t H5E_OPENERROR_g = H5DLLImporter.Instance.GetHid("H5E_OPENERROR_g");

    public static hid_t OPENERROR { get { return H5E_OPENERROR_g; } }

    static readonly hid_t H5E_FILEEXISTS_g = H5DLLImporter.Instance.GetHid("H5E_FILEEXISTS_g");

    public static hid_t FILEEXISTS { get { return H5E_FILEEXISTS_g; } }

    static readonly hid_t H5E_FILEOPEN_g = H5DLLImporter.Instance.GetHid("H5E_FILEOPEN_g");

    public static hid_t FILEOPEN { get { return H5E_FILEOPEN_g; } }

    static readonly hid_t H5E_CANTCREATE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCREATE_g");

    public static hid_t CANTCREATE { get { return H5E_CANTCREATE_g; } }

    static readonly hid_t H5E_CANTOPENFILE_g = H5DLLImporter.Instance.GetHid("H5E_CANTOPENFILE_g");

    public static hid_t CANTOPENFILE { get { return H5E_CANTOPENFILE_g; } }

    static readonly hid_t H5E_CANTCLOSEFILE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCLOSEFILE_g");

    public static hid_t CANTCLOSEFILE { get { return H5E_CANTCLOSEFILE_g; } }

    static readonly hid_t H5E_NOTHDF5_g = H5DLLImporter.Instance.GetHid("H5E_NOTHDF5_g");

    public static hid_t NOTHDF5 { get { return H5E_NOTHDF5_g; } }

    static readonly hid_t H5E_BADFILE_g = H5DLLImporter.Instance.GetHid("H5E_BADFILE_g");

    public static hid_t BADFILE { get { return H5E_BADFILE_g; } }

    static readonly hid_t H5E_TRUNCATED_g = H5DLLImporter.Instance.GetHid("H5E_TRUNCATED_g");

    public static hid_t TRUNCATED { get { return H5E_TRUNCATED_g; } }

    static readonly hid_t H5E_MOUNT_g = H5DLLImporter.Instance.GetHid("H5E_MOUNT_g");

    public static hid_t MOUNT { get { return H5E_MOUNT_g; } }

    static readonly hid_t H5E_BADATOM_g = H5DLLImporter.Instance.GetHid("H5E_BADATOM_g");

    public static hid_t BADATOM { get { return H5E_BADATOM_g; } }

    static readonly hid_t H5E_BADGROUP_g = H5DLLImporter.Instance.GetHid("H5E_BADGROUP_g");

    public static hid_t BADGROUP { get { return H5E_BADGROUP_g; } }

    static readonly hid_t H5E_CANTREGISTER_g = H5DLLImporter.Instance.GetHid("H5E_CANTREGISTER_g");

    public static hid_t CANTREGISTER { get { return H5E_CANTREGISTER_g; } }

    static readonly hid_t H5E_CANTINC_g = H5DLLImporter.Instance.GetHid("H5E_CANTINC_g");

    public static hid_t CANTINC { get { return H5E_CANTINC_g; } }

    static readonly hid_t H5E_CANTDEC_g = H5DLLImporter.Instance.GetHid("H5E_CANTDEC_g");

    public static hid_t CANTDEC { get { return H5E_CANTDEC_g; } }

    static readonly hid_t H5E_NOIDS_g = H5DLLImporter.Instance.GetHid("H5E_NOIDS_g");

    public static hid_t NOIDS { get { return H5E_NOIDS_g; } }

    static readonly hid_t H5E_CANTFLUSH_g = H5DLLImporter.Instance.GetHid("H5E_CANTFLUSH_g");

    public static hid_t CANTFLUSH { get { return H5E_CANTFLUSH_g; } }

    static readonly hid_t H5E_CANTSERIALIZE_g = H5DLLImporter.Instance.GetHid("H5E_CANTSERIALIZE_g");

    public static hid_t CANTSERIALIZE { get { return H5E_CANTSERIALIZE_g; } }

    static readonly hid_t H5E_CANTLOAD_g = H5DLLImporter.Instance.GetHid("H5E_CANTLOAD_g");

    public static hid_t CANTLOAD { get { return H5E_CANTLOAD_g; } }

    static readonly hid_t H5E_PROTECT_g = H5DLLImporter.Instance.GetHid("H5E_PROTECT_g");

    public static hid_t PROTECT { get { return H5E_PROTECT_g; } }

    static readonly hid_t H5E_NOTCACHED_g = H5DLLImporter.Instance.GetHid("H5E_NOTCACHED_g");

    public static hid_t NOTCACHED { get { return H5E_NOTCACHED_g; } }

    static readonly hid_t H5E_SYSTEM_g = H5DLLImporter.Instance.GetHid("H5E_SYSTEM_g");

    public static hid_t SYSTEM { get { return H5E_SYSTEM_g; } }

    static readonly hid_t H5E_CANTINS_g = H5DLLImporter.Instance.GetHid("H5E_CANTINS_g");

    public static hid_t CANTINS { get { return H5E_CANTINS_g; } }

    static readonly hid_t H5E_CANTPROTECT_g = H5DLLImporter.Instance.GetHid("H5E_CANTPROTECT_g");

    public static hid_t CANTPROTECT { get { return H5E_CANTPROTECT_g; } }

    static readonly hid_t H5E_CANTUNPROTECT_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNPROTECT_g");

    public static hid_t CANTUNPROTECT { get { return H5E_CANTUNPROTECT_g; } }

    static readonly hid_t H5E_CANTPIN_g = H5DLLImporter.Instance.GetHid("H5E_CANTPIN_g");

    public static hid_t CANTPIN { get { return H5E_CANTPIN_g; } }

    static readonly hid_t H5E_CANTUNPIN_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNPIN_g");

    public static hid_t CANTUNPIN { get { return H5E_CANTUNPIN_g; } }

    static readonly hid_t H5E_CANTMARKDIRTY_g = H5DLLImporter.Instance.GetHid("H5E_CANTMARKDIRTY_g");

    public static hid_t CANTMARKDIRTY { get { return H5E_CANTMARKDIRTY_g; } }

    static readonly hid_t H5E_CANTDIRTY_g = H5DLLImporter.Instance.GetHid("H5E_CANTDIRTY_g");

    public static hid_t CANTDIRTY { get { return H5E_CANTDIRTY_g; } }

    static readonly hid_t H5E_CANTEXPUNGE_g = H5DLLImporter.Instance.GetHid("H5E_CANTEXPUNGE_g");

    public static hid_t CANTEXPUNGE { get { return H5E_CANTEXPUNGE_g; } }

    static readonly hid_t H5E_CANTRESIZE_g = H5DLLImporter.Instance.GetHid("H5E_CANTRESIZE_g");

    public static hid_t CANTRESIZE { get { return H5E_CANTRESIZE_g; } }

    static readonly hid_t H5E_TRAVERSE_g = H5DLLImporter.Instance.GetHid("H5E_TRAVERSE_g");

    public static hid_t TRAVERSE { get { return H5E_TRAVERSE_g; } }

    static readonly hid_t H5E_NLINKS_g = H5DLLImporter.Instance.GetHid("H5E_NLINKS_g");

    public static hid_t NLINKS { get { return H5E_NLINKS_g; } }

    static readonly hid_t H5E_NOTREGISTERED_g = H5DLLImporter.Instance.GetHid("H5E_NOTREGISTERED_g");

    public static hid_t NOTREGISTERED { get { return H5E_NOTREGISTERED_g; } }

    static readonly hid_t H5E_CANTMOVE_g = H5DLLImporter.Instance.GetHid("H5E_CANTMOVE_g");

    public static hid_t CANTMOVE { get { return H5E_CANTMOVE_g; } }

    static readonly hid_t H5E_CANTSORT_g = H5DLLImporter.Instance.GetHid("H5E_CANTSORT_g");

    public static hid_t CANTSORT { get { return H5E_CANTSORT_g; } }

    static readonly hid_t H5E_MPI_g = H5DLLImporter.Instance.GetHid("H5E_MPI_g");

    public static hid_t MPI { get { return H5E_MPI_g; } }

    static readonly hid_t H5E_MPIERRSTR_g = H5DLLImporter.Instance.GetHid("H5E_MPIERRSTR_g");

    public static hid_t MPIERRSTR { get { return H5E_MPIERRSTR_g; } }

    static readonly hid_t H5E_CANTRECV_g = H5DLLImporter.Instance.GetHid("H5E_CANTRECV_g");

    public static hid_t CANTRECV { get { return H5E_CANTRECV_g; } }

    static readonly hid_t H5E_CANTCLIP_g = H5DLLImporter.Instance.GetHid("H5E_CANTCLIP_g");

    public static hid_t CANTCLIP { get { return H5E_CANTCLIP_g; } }

    static readonly hid_t H5E_CANTCOUNT_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOUNT_g");

    public static hid_t CANTCOUNT { get { return H5E_CANTCOUNT_g; } }

    static readonly hid_t H5E_CANTSELECT_g = H5DLLImporter.Instance.GetHid("H5E_CANTSELECT_g");

    public static hid_t CANTSELECT { get { return H5E_CANTSELECT_g; } }

    static readonly hid_t H5E_CANTNEXT_g = H5DLLImporter.Instance.GetHid("H5E_CANTNEXT_g");

    public static hid_t CANTNEXT { get { return H5E_CANTNEXT_g; } }

    static readonly hid_t H5E_BADSELECT_g = H5DLLImporter.Instance.GetHid("H5E_BADSELECT_g");

    public static hid_t BADSELECT { get { return H5E_BADSELECT_g; } }

    static readonly hid_t H5E_CANTCOMPARE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOMPARE_g");

    public static hid_t CANTCOMPARE { get { return H5E_CANTCOMPARE_g; } }

    static readonly hid_t H5E_UNINITIALIZED_g = H5DLLImporter.Instance.GetHid("H5E_UNINITIALIZED_g");

    public static hid_t UNINITIALIZED { get { return H5E_UNINITIALIZED_g; } }

    static readonly hid_t H5E_UNSUPPORTED_g = H5DLLImporter.Instance.GetHid("H5E_UNSUPPORTED_g");

    public static hid_t UNSUPPORTED { get { return H5E_UNSUPPORTED_g; } }

    static readonly hid_t H5E_BADTYPE_g = H5DLLImporter.Instance.GetHid("H5E_BADTYPE_g");

    public static hid_t BADTYPE { get { return H5E_BADTYPE_g; } }

    static readonly hid_t H5E_BADRANGE_g = H5DLLImporter.Instance.GetHid("H5E_BADRANGE_g");

    public static hid_t BADRANGE { get { return H5E_BADRANGE_g; } }

    static readonly hid_t H5E_BADVALUE_g = H5DLLImporter.Instance.GetHid("H5E_BADVALUE_g");

    public static hid_t BADVALUE { get { return H5E_BADVALUE_g; } }

    static readonly hid_t H5E_NOTFOUND_g = H5DLLImporter.Instance.GetHid("H5E_NOTFOUND_g");

    public static hid_t NOTFOUND { get { return H5E_NOTFOUND_g; } }

    static readonly hid_t H5E_EXISTS_g = H5DLLImporter.Instance.GetHid("H5E_EXISTS_g");

    public static hid_t EXISTS { get { return H5E_EXISTS_g; } }

    static readonly hid_t H5E_CANTENCODE_g = H5DLLImporter.Instance.GetHid("H5E_CANTENCODE_g");

    public static hid_t CANTENCODE { get { return H5E_CANTENCODE_g; } }

    static readonly hid_t H5E_CANTDECODE_g = H5DLLImporter.Instance.GetHid("H5E_CANTDECODE_g");

    public static hid_t CANTDECODE { get { return H5E_CANTDECODE_g; } }

    static readonly hid_t H5E_CANTSPLIT_g = H5DLLImporter.Instance.GetHid("H5E_CANTSPLIT_g");

    public static hid_t CANTSPLIT { get { return H5E_CANTSPLIT_g; } }

    static readonly hid_t H5E_CANTREDISTRIBUTE_g = H5DLLImporter.Instance.GetHid("H5E_CANTREDISTRIBUTE_g");

    public static hid_t CANTREDISTRIBUTE { get { return H5E_CANTREDISTRIBUTE_g; } }

    static readonly hid_t H5E_CANTSWAP_g = H5DLLImporter.Instance.GetHid("H5E_CANTSWAP_g");

    public static hid_t CANTSWAP { get { return H5E_CANTSWAP_g; } }

    static readonly hid_t H5E_CANTINSERT_g = H5DLLImporter.Instance.GetHid("H5E_CANTINSERT_g");

    public static hid_t CANTINSERT { get { return H5E_CANTINSERT_g; } }

    static readonly hid_t H5E_CANTLIST_g = H5DLLImporter.Instance.GetHid("H5E_CANTLIST_g");

    public static hid_t CANTLIST { get { return H5E_CANTLIST_g; } }

    static readonly hid_t H5E_CANTMODIFY_g = H5DLLImporter.Instance.GetHid("H5E_CANTMODIFY_g");

    public static hid_t CANTMODIFY { get { return H5E_CANTMODIFY_g; } }

    static readonly hid_t H5E_CANTREMOVE_g = H5DLLImporter.Instance.GetHid("H5E_CANTREMOVE_g");

    public static hid_t CANTREMOVE { get { return H5E_CANTREMOVE_g; } }

    static readonly hid_t H5E_CANTCONVERT_g = H5DLLImporter.Instance.GetHid("H5E_CANTCONVERT_g");

    public static hid_t CANTCONVERT { get { return H5E_CANTCONVERT_g; } }

    static readonly hid_t H5E_BADSIZE_g = H5DLLImporter.Instance.GetHid("H5E_BADSIZE_g");

    public static hid_t BADSIZE { get { return H5E_BADSIZE_g; } }
}
