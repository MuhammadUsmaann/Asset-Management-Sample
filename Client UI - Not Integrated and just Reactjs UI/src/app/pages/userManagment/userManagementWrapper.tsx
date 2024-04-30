import { FC, useState } from 'react'
import { CreateUserModal } from '../../../_metronic/partials'
// import Pagination from '../../components/table/pagination'

const UserManagementWrapper: FC = () => {

    const [showCreateAppModal, setShowCreateAppModal] = useState(false)
    return (
        <>
            <div className="p-10">
                <div className="d-flex justify-content-between">
                    <p className='fs-2 fw-bold text-gray-800'>User Management</p>
                    <div className=''>
                        <a
                            href='#'
                            onClick={() => setShowCreateAppModal(true)}
                            className='btn btn-sm fw-bold btn-primary'
                        >
                            Create User
                        </a>    
                    </div>
                </div>
                <div className='table-responsive py-5'>
                    {/* begin::Table */}
                    <table className='table align-middle gs-0 gy-4'>
                        {/* begin::Table head */}
                        <thead>
                            <tr className='fw-bold text-gray-900 bg-gray-200'>
                                <th className={'ps-4 rounded-start'} >Name</th>
                                <th className={'ps-4'} >Phone No.</th>
                                <th className={'ps-4'} >Email</th>
                                <th className={'ps-4'} >Role</th>
                                <th className={'ps-4 rounded-end'} >Team</th>
                            </tr>
                        </thead>
                        {/* end::Table head */}
                        {/* begin::Table body */}
                        <tbody>
                            <tr className=''>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6 ps-4'}>Muhamamd Usman</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>+92300 4615234</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>muhammad.usmaann@gmail.com</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>User</a></td>
                                <td>
                                    <select
                                        className='form-select form-select-solid form-select-lg fw-bold'
                                    >
                                        <option value='A'>Team A</option>
                                        <option value='B'>Team B</option>
                                        <option value='C'>Team C</option>
                                        <option value='D'>Team D</option>

                                    </select>
                                </td>
                            </tr>
                            <tr className=''>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6 ps-4'}>Max</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>+92300 4615234</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>max@gmail.com</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>User</a></td>
                                <td>
                                    <select
                                        className='form-select form-select-solid form-select-lg fw-bold'
                                    >
                                        <option value='A'>Team A</option>
                                        <option value='B'>Team B</option>
                                        <option value='C'>Team C</option>
                                        <option value='D'>Team D</option>

                                    </select>
                                </td>
                            </tr>
                            <tr className=''>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6 ps-4'}>Thomas</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>+92300 4615234</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>thomas@gmail.com</a></td>
                                <td><a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6'}>User</a></td>
                                <td>
                                    <select
                                        className='form-select form-select-solid form-select-lg fw-bold'
                                    >
                                        <option value='A'>Team A</option>
                                        <option value='B'>Team B</option>
                                        <option value='C'>Team C</option>
                                        <option value='D'>Team D</option>

                                    </select>
                                </td>
                            </tr>
                        </tbody>
                        {/* end::Table body */}
                    </table>
                    {/* end::Table */}

                    {/* <Pagination /> */}

                </div >
            </div >
            <CreateUserModal show={showCreateAppModal} handleClose={() => setShowCreateAppModal(false)} />

        </>
    )
}

export { UserManagementWrapper }
